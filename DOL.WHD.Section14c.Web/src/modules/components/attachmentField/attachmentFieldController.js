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
    vm.stateService = stateService;
    vm.apiService = apiService;
    $scope.formData = stateService.formData;
    if(!$scope.formData[$scope.modelPrefix][$scope.inputId]) {
      stateService.formData[$scope.modelPrefix][$scope.inputId] = [];
    }
    $scope.restrictUpload = false;
    $scope.upload = {
      status: "NoFile",
      message: 'No file is selected.'
    }

    $scope.allowedFileTypes = _env.allowedFileTypes;

    this.onAttachmentSelected = function(fileinput) {
      $scope.upload.status = "Uploading";
      $scope.upload.message = 'File is uploading.'
      $scope.attachmentName = fileinput.files[0].name;
      
      if (fileinput && vm.validateAttachment(fileinput.files[0], $scope.allowedFileTypes)) {
        vm.uploadAttachment(fileinput);
      }
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
        apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function(result) {
          $scope.upload.status = 'Success';
          $scope.upload.message = 'File was uploaded successfully.'
          $scope.attachmentId = result.data[0].id;
          $scope.attachmentName = result.data[0].originalFileName;
          var attachment = {};
          attachment.attachmentId = result.data[0].id;
          attachment.attachmentName = result.data[0].originalFileName;
          fileinput.value = '';
          if($scope.allowMultiUpload) {
            $scope.formData[$scope.modelPrefix][$scope.inputId].push(attachment);
          } else {
              if($scope.formData[$scope.modelPrefix][$scope.inputId][0]) {
                vm.deleteAttachment($scope.formData[$scope.modelPrefix][$scope.inputId][0].attachmentId);
              }
              $scope.formData[$scope.modelPrefix][$scope.inputId][0] = attachment;
          }

          if(!$scope.allowMultiUpload) {
            $scope.restrictUpload = true;
          }
        }).catch(function(error) {
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

      apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function() {
        $scope.restrictUpload = false;
        $scope.upload.status = 'NoFile';
        $scope.attachmentId = undefined;
        $scope.attachmentName = undefined;
        $scope.formData[$scope.modelPrefix][$scope.inputId].forEach(function(element, index) {
          if (element.attachmentId === id) {
            $scope.formData[$scope.modelPrefix][$scope.inputId].splice(index, 1);
          }
        });
        vm.hideModal();
      }).catch(function(error) {
        //TODO: Display error
        console.log(error)
        $scope.upload.status = 'Failure'
        $scope.attachmentId = undefined;
        $scope.attachmentName = undefined;
      });
    };

    // $('.modal-trigger').on('click', function(event){
    //   panelTrigger = $(this);
    //   var target = $(this).attr('aria-controls');
    //   $(`#${target}`).addClass('is-visible');
    //   $(`#${target} .modal-header h3`).focus();
    //   vm.clearActiveWorker();
    //   $('body').addClass('modal-open');
    //   event.preventDefault();
    // });

  });
};
