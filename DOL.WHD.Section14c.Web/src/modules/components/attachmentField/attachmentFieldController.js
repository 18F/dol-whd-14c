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
    $scope.restrictUpload = false;
    if(!$scope.attachmentId) {
       vm.upload = {
        status: "NoFile",
        message: 'No file is selected.'
      }
    } else {
      vm.upload = {
        status: "Success",
        message: 'File was uploaded successfully.'
      }
    }

    $scope.allowedFileTypes = _env.allowedFileTypes;

    this.onAttachmentSelected = function(fileinput) {
      vm.upload.status = "Uploading";
      vm.upload.message = 'File is uploading.'
      if (fileinput && vm.validateAttachment(fileinput.files[0], $scope.allowedFileTypes)) {
        vm.uploadAttachment(fileinput);
      }
    };

    this.validateAttachment = function (fileinput, allowedFileTypes) {
      var ext = fileinput.name.split(".").pop();
      if(allowedFileTypes.indexOf(ext) < 0) {
        vm.upload.status = 'Invalid';
        vm.upload.message = 'Invalid File Type.';
        fileinput.value = '';
        return false;
      }
      if (fileinput.size / 1024000 > 5) {
        vm.upload.status = 'Invalid';
        vm.upload.message = 'File Size too large.';
        fileinput.value = '';
        return false;
      }
      return true;
    };

    this.uploadAttachment = function (fileinput) {
      if(vm.upload.status != 'Invalid') {
        apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function(result) {
          $scope.restrictUpload = true;
          vm.upload.status = 'Success';
          vm.upload.message = 'File was uploaded successfully.'
          $scope.attachmentId = result.data[0].id;
          $scope.attachmentName = result.data[0].originalFileName;
          fileinput.value = '';
        }).catch(function(error) {
          fileinput.value = '';
          vm.upload.status = 'Server Error';
          vm.upload.message = error.statusMessage;
          $scope.attachmentId = undefined;
          $scope.attachmentName = undefined;
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
        vm.upload.status = 'NoFile';
        $scope.attachmentId = undefined;
        $scope.attachmentName = undefined;
      }).catch(function() {
        //TODO: Display error
        vm.upload.status = 'Failure'
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
