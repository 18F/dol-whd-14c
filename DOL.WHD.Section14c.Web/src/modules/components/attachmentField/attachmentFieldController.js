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
    vm.upload = {
      status: "NoFile",
      message: 'No file is selected.'
    }


    this.onAttachmentSelected = function(fileinput) {
      vm.upload = {
        status: "Uploading",
        message: 'File is uploading.'
      }
      $scope.$apply()
      if (fileinput.files.length > 0) {
        var ext = fileinput.files[0].name.split(".")[1];
        var allowedFileTypes = ['pdf', 'jpg', 'jpeg', 'png', 'csv', 'PDF'];


        if(allowedFileTypes.indexOf(ext) < 0) {
          vm.upload.status = 'Invalid';
          vm.upload.message = 'Invalid File Type.';
          $scope.attachmentName = fileinput.files[0].name;
        }

        if ((fileinput.files[0].size / 1024) / 1000 > 5) {
          vm.upload.status = 'Invalid';
          vm.upload.message = 'File Size too large.';
          $scope.attachmentName = fileinput.files[0].name;

        }
        if(vm.upload.status != 'Invalid') {
          apiService
          .uploadAttachment(
            stateService.access_token,
            stateService.ein,
            fileinput.files[0]
          )
          .then(
            function(result) {
              vm.upload.status = 'Success';
              $scope.attachmentId = result.data[0].id;
              $scope.attachmentName = result.data[0].originalFileName;
              fileinput.value = '';
            },
            function(error) {
              //TODO: Display error
              fileinput.value = '';
              vm.upload.status = 'Invalid';
              vm.upload.message = error.statusMessage;
            }
          );
        }
      }
    };

    this.deleteAttachment = function(id) {
      apiService
        .deleteAttachment(stateService.access_token, stateService.ein, id)
        .then(
          function() {
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
