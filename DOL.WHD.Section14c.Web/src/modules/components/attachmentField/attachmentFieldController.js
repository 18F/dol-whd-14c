'use strict';
/* eslint-disable complexity */
module.exports = function(ngModule) {
  ngModule.controller('attachmentFieldController', function(
    $scope,
    apiService,
    _env,
    stateService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    $scope.formData = stateService.formData;
    if(!$scope.attachments) {
      $scope.attachments = [];
    }
    console.log($scope.attachments)
    $scope.restrictUpload = false;
    $scope.upload = {
      status: "NoFile",
      message: 'No file is selected.'
    }

    $scope.allowedFileTypes = _env.allowedFileTypes;

    this.onAttachmentSelected = function(fileinput) {
      if(fileinput) {
        $scope.attachmentName = fileinput.files[0].name
      } else {
        $scope.attachmentName = '';
        $scope.upload.status = 'NoFile';
        $scope.upload.message = 'No file is selected.' ;
        return;
      }
      $scope.upload.status = "Uploading";
      $scope.upload.message = 'File is uploading.';

      if (fileinput && vm.validateAttachment(fileinput.files[0], $scope.allowedFileTypes)) {
        vm.uploadAttachment(fileinput);
      }
      fileinput.value = '';
      $scope.$apply();
    };

    this.validateAttachment = function (fileinput, allowedFileTypes) {
      var ext = fileinput.name.split(".").pop();
      if(allowedFileTypes.indexOf(ext) < 0) {
        $scope.upload.status = 'Invalid';
        $scope.upload.message = 'Invalid File Type.';
        fileinput.value = '';
        return false;
      }
      if (fileinput.size / 1024000 > 5) {
        $scope.upload.status = 'Invalid';
        $scope.upload.message = 'File Size too large.';
        fileinput.value = '';
        return false;
      }
      return true;
    };

    this.uploadAttachment = function (fileinput) {
      if($scope.upload.status != 'Invalid') {

        apiService.uploadAttachment(stateService.access_token, stateService.applicationId, fileinput.files[0]).then(function(result) {
          $scope.upload.status = 'Success';
          $scope.upload.message = 'File was uploaded successfully.'
          $scope.attachmentId = result.data[0].id;
          $scope.attachmentName = result.data[0].originalFileName;
          var attachment = {};
          attachment.attachmentId = result.data[0].id;
          attachment.attachmentName = result.data[0].originalFileName;
          fileinput.value = '';
          console.log($scope.allowMultiUpload)
          if($scope.allowMultiUpload) {
            $scope.attachments.push(attachment);
          } else {
              if($scope.attachments[0]) {
                console.log('here')
                vm.deleteAttachment($scope.attachments[0].attachmentId).then(function(result) {
                  console.log(result)
                }).catch(function(error){
                  console.log(error)
                });
              }
              console.log($scope.formData, $scope.inputId)
              $scope.attachments = attachment;
          }
          if(!$scope.allowMultiUpload) {
            $scope.restrictUpload = true;
          }
        }).catch(function(error) {
          console.log('here')
          fileinput.value = '';
          $scope.upload.status = 'Server Error';
          $scope.upload.message = error.statusMessage;
          $scope.attachmentId = undefined;
          $scope.attachmentName = undefined;
        });
      }
    }

    $scope.modalIsVisible = false;

    this.showModal = function (id, attachmentName) {
      //$('.modal').addClass('is-visible');
      $scope.attachmentName = attachmentName;
      $scope.attachmentId = id;
      $scope.modalIsVisible = true;
      $scope.upload.status = "Deleting"
      $scope.upload.message = "Deleting attachment."
    };

    this.hideModal = function() {
     // $('.modal').removeClass('is-visible');
      $scope.modalIsVisible = false;
      $scope.upload.status = "NoFile"
      $scope.upload.message = "No file Selected."
    }

    this.deleteAttachment = function(id) {
      console.log('here')
      apiService.deleteAttachment(stateService.access_token, stateService.applicationId, id).then(function() {
        $scope.restrictUpload = false;
        $scope.upload.status = 'NoFile';
        $scope.attachmentId = undefined;
        $scope.attachmentName = undefined;
        vm.hideModal();
      }).catch(function() {
        //TODO: Display error
        $scope.upload.status = 'Failure'
        $scope.attachmentId = undefined;
        $scope.attachmentName = undefined;
      });
    };
  });
};
