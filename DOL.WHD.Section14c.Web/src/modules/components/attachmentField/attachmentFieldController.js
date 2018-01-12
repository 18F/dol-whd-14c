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
    vm.uploadError = {
      status: false,
      message: ''
    }
    

    this.onAttachmentSelected = function(fileinput) {
      vm.uploadError.status = false;
      vm.uploadError.message = '';
      console.log(fileinput.files[0].size);
      console.log(fileinput.files[0].name);

      var ext = fileinput.files[0].name.split(".")[1];
      var allowedFileTypes = ['pdf', 'jpg', 'jpeg', 'png', 'csv', 'PDF'];

      console.log(ext);
      console.log(allowedFileTypes.indexOf(ext));
      console.log((fileinput.files[0].size / 1024) / 1000 > 5);

      if(allowedFileTypes.indexOf(ext) < 0) {
        vm.uploadError.status = true;
        vm.uploadError.message = 'Invalid file type';
      } 
      
      if ((fileinput.files[0].size / 1024) / 1000 > 5) {
        vm.uploadError.status = true;
        vm.uploadError.message = 'Invalid file size';
        console.log('in fileSize if statement');
        console.log($scope.attachmentId);
      }

      if (fileinput.files.length > 0) {
        apiService
          .uploadAttachment(
            stateService.access_token,
            stateService.ein,
            fileinput.files[0]
          )
          .then(
            function(result) {
              $scope.attachmentId = result.data[0].id;
              $scope.attachmentName = result.data[0].originalFileName;
              fileinput.value = '';
              console.log('in then block');
            },
            function(error) {
              //TODO: Display error
              fileinput.value = '';
              vm.uploadError.message = error.statusMessage;
              console.log('in api catch');
            }
          );
      }
    };

    this.deleteAttachment = function(id) {
      apiService
        .deleteAttachment(stateService.access_token, stateService.ein, id)
        .then(
          function() {
            $scope.attachmentId = undefined;
            $scope.attachmentName = undefined;
          },
          function() {
            //TODO: Display error
            $scope.attachmentId = undefined;
            $scope.attachmentName = undefined;
          }
        );
    };
  });
};
