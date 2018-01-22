'use strict';
/* eslint-disable complexity */
module.exports = function(ngModule) {
  ngModule.controller('attachmentsFieldController', function(
    $scope,
    apiService,
    _env,
    autoSaveService,
    stateService,
    $window
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    $scope.formData = stateService.formData;
    if(!$scope.attachments) {
      $scope.attachments = [];
    }
    console.log(stateService.user)

    $scope.restrictUpload = false;
    $scope.upload = {
      status: "NoFile",
      message: 'No file is selected.'
    };

    $scope.allowedFileTypes = _env.allowedFileTypes;

    this.setActiveAttachment = function(id, name) {
      $scope.attachmentId = id;
      $scope.attachmentName = name;
    };

    this.setUploadStatus = function (status, message) {
      $scope.upload.status = status;
      $scope.upload.message = message;
    };

    this.downloadAttachment = function(id) {
        var downloadURL = _env.api_url + '/api/attachment/' + stateService.applicationId + '/' + id + '?access_token=' + stateService.access_token;
        $window.location.href = downloadURL;
    };

    this.onAttachmentSelected = function(fileinput) {
      if(fileinput) {
        vm.setActiveAttachment(null, fileinput.files[0].name);
      } else {
        vm.setActiveAttachment();
        vm.setUploadStatus('NoFile', 'No file is selected.');
        return;
      }
      vm.setUploadStatus("Uploading", "File is uploading.");
      if (fileinput && vm.validateAttachment(fileinput.files[0], $scope.allowedFileTypes)) {
        vm.uploadAttachment(fileinput);
      }
      fileinput.value = '';
      $scope.$apply();
    };

    this.validateAttachment = function (fileinput, allowedFileTypes) {
      var ext = fileinput.name.split(".").pop();
      if(allowedFileTypes.indexOf(ext) < 0) {
        vm.setUploadStatus("Invalid", "Invalid File Type.");
        fileinput.value = '';
        return false;
      }
      if (fileinput.size / 1024000 > 5) {
        vm.setUploadStatus("Invalid", "File Size too large.");
        fileinput.value = '';
        return false;
      }
      return true;
    };

    this.uploadAttachment = function (fileinput) {
      if($scope.upload.status != 'Invalid') {

        apiService.uploadAttachment(stateService.access_token, stateService.applicationId, fileinput.files[0]).then(function(result) {
          vm.setUploadStatus("Success", "File was uploaded successfully.");
          vm.setActiveAttachment(result.data[0].id, result.data[0].originalFileName);
          var attachment = {};
          attachment.attachmentId = result.data[0].id;
          attachment.employerInfoId = result.data[0].originalFileName;
          fileinput.value = '';
          if($scope.allowMultiUpload) {
            $scope.attachments.push(attachment);
          } else {
              if($scope.attachments[0]) {
                vm.deleteAttachment($scope.attachments[0].attachmentId);
              }
              $scope.attachments[0] = attachment;
          }
          if(!$scope.allowMultiUpload) {
            $scope.restrictUpload = true;
          }
        }).catch(function(error) {
          fileinput.value = '';
          vm.setUploadStatus("Server Error", error.statusMessage);
          vm.setActiveAttachment();
        });
      }

      autoSaveService.save()
    }

    $scope.modalIsVisible = false;

    this.showModal = function (id, attachmentName) {
      //$('.modal').addClass('is-visible');
      $scope.attachmentName = attachmentName;
      $scope.attachmentId = id;
      $scope.modalIsVisible = true;
      vm.setUploadStatus('Deleting', 'Attempting to delete file.');
    };

    this.hideModal = function() {
     // $('.modal').removeClass('is-visible');
      $scope.modalIsVisible = false;
      vm.setUploadStatus('NoFile', 'No file is selected.');
    }

    this.deleteAttachment = function(id, name) {
      $scope.attachmentName = name;
      apiService.deleteAttachment(stateService.access_token, stateService.applicationId, id).then(function() {
        $scope.restrictUpload = false;
        vm.setUploadStatus('NoFile', 'No file is selected.');
        vm.setActiveAttachment();
        var index = 0;
        $scope.attachments.forEach(function(element, $index) {
          if(element.attachmentId === id) {
            index = $index;
          }
        });

        $scope.attachments.splice(index, 1);
        vm.hideModal();
      }).catch(function() {
        //TODO: Display error
        vm.setUploadStatus('Failure', 'Failed to delete file');

      });

      autoSaveService.save()
    };
  });
};
