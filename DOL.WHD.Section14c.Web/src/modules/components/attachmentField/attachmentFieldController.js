'use strict';

module.exports = function(ngModule) {
  ngModule.controller('attachmentFieldController', function(
    $scope,
    apiService,
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
    
    this.allowedFileTypes = ['pdf', 'jpg', 'JPG', 'jpeg', 'JPEG', 'png', 'PNG', 'csv', 'CSV', 'PDF'];

    this.onAttachmentSelected = function(fileinput) {
      vm.upload.status = "Uploading";
      vm.upload.message = 'File is uploading.'
      if (fileinput.files.length > 0 ) {
        vm.validateAttachment(fileinput.files[0], vm.allowedFileTypes);
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
            });
        }
      }
    };

    this.validateAttachment = function (fileinput, allowedFileTypes) {
      var ext = fileinput.name.split(".")[1];
      if(allowedFileTypes.indexOf(ext) < 0) {
        vm.upload.status = 'Invalid';
        vm.upload.message = 'Invalid File Type.';
        return false;
      }
      if ((fileinput.size / 1024000 > 5) {
        vm.upload.status = 'Invalid';
        vm.upload.message = 'File Size too large.';
        return false;
      }
      return true;
    };

    this.deleteAttachment = function(id) {
      apiService
        .deleteAttachment(stateService.access_token, stateService.ein, id)
        .then(
          function() {
            $scope.restrictUpload = false;
            vm.upload.status = 'NoFile';
            $scope.attachmentId = undefined;
            $scope.attachmentName = undefined;
          },
          function() {
            //TODO: Display error
            vm.upload.status = 'Failure'
            $scope.attachmentId = undefined;
            $scope.attachmentName = undefined;
          }
        );
    };
  });
};
