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
    // if(!$scope.attachmentId) {
    //    vm.upload = {
    //     status: "NoFile",
    //     message: 'No file is selected.'
    //   }
    // } else {
    //   vm.upload = {
    //     status: "Success",
    //     message: 'File was uploaded successfully.'
    //   }
    // }

    $scope.allowedFileTypes = _env.allowedFileTypes;

    this.onAttachmentSelected = function(input) {
      var attachment = {};
      attachment.upload = vm.upload;
      attachment.upload.status = "Uploading";
      attachment.upload.message = 'File is uploading.'
      var validation = vm.validationAttachment(input.files[0], $scope.allowedFileTypes);
      attachment.upload.status = validation.status;
      attachment.upload.message = validation.message;
      if (input && attachment.upload.status) {
        vm.uploadAttachment(input.files[0], attachment);
      }
    };

    this.validateAttachment = function (fileinput, allowedFileTypes) {
      var ext = fileinput.name.split(".").pop();
      if(allowedFileTypes.indexOf(ext) < 0) {
        vm.upload.status = 'Invalid';
        vm.upload.message = 'Invalid File Type.';
        fileinput.value = '';
        return {
          status: 'Invalid',
          message: 'Invalid File Type.'
        };
      }
      if (fileinput.size / 1024000 > 5) {
        vm.upload.status = 'Invalid';
        vm.upload.message = 'File Size too large.';
        fileinput.value = '';
        return {
          status: 'Invalid',
          message: 'File Size too large.'
        };
      }
      return {
        status: "Uploading",
        message: 'File is uploading.'
      };
    };

    this.uploadAttachment = function (fileinput, attachment) {
      if(attachment.upload.status != 'Invalid') {
        apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput).then(function(result) {
          attachment.attachmentId = result.data[0].id;
          attachment.attachmentName = result.data[0].originalFileName;
          fileinput.value = '';
          $scope.restrictUpload = true;
          attachment.upload.status = 'Success';
          attachment.upload.message = 'File was uploaded successfully.'
          if($scope.inputId === "prScaWageDeterminationAttachments" || $scope.inputId === "hScaWageDeterminationAttachments") {
             $scope.formData[$scope.modelPrefix][$scope.inputId].push(attachment);
          } else {
              if($scope.formData[$scope.modelPrefix][$scope.inputId][0]) {
               vm.deleteAttachment($scope.formData[$scope.modelPrefix][$scope.inputId][0].attachmentId);
              }

              $scope.formData[$scope.modelPrefix][$scope.inputId][0] = attachment;
          }
        }).catch(function(error) {
          fileinput.value = '';
          attachment.upload.status = 'Server Error';
          attachment.upload.message = error.statusMessage;
        });
      }
    }

    this.showModal = function () {
      $('.modal').addClass('is-visible');
    };

    this.hideModal = function() {
      $('.modal').removeClass('is-visible');

    }

    this.deleteAttachment = function(id) {
      apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function() {
        $scope.restrictUpload = false;
        $scope.formData[$scope.modelPrefix][$scope.inputId].forEach(function(element, index) {
          if (element.attachmentId === id) {
            $scope.formData[$scope.modelPrefix][$scope.inputId][index].attachmentId = undefined;
            $scope.formData[$scope.modelPrefix][$scope.inputId][index].attachmentName = undefined;
            $scope.formData[$scope.modelPrefix][$scope.inputId].splice(index, 1);
          }
        });
      }).catch(function() {
        //TODO: Display error
        // vm.upload.status = 'Failure'
        // $scope.attachmentId = undefined;
        // $scope.attachmentName = undefined;
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
