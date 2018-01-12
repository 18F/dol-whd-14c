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
    
    

    this.onAttachmentSelected = function(fileinput) {
      $scope.upload = {
        status: undefined,
        message: ''
      }

      var ext = fileinput.files[0].name.split(".")[1];
      var allowedFileTypes = ['pdf', 'jpg', 'jpeg', 'png', 'csv', 'PDF'];


      if(allowedFileTypes.indexOf(ext) < 0) {
        $scope.upload.status = 'Unsuccessful';
        $scope.upload.message = 'Invalid file type.';
        $scope.attachmentName = fileinput.files[0].name;
      } 
      
      if ((fileinput.files[0].size / 1024) / 1000 > 5) {
        $scope.upload.status = 'Unsuccessful';
        $scope.upload.message = 'Invalid file size.';
        $scope.attachmentName = fileinput.files[0].name;
        console.log($scope.upload.status === 'Unsuccessful');
      }
      console.log($scope.upload.status);

      if (fileinput.files.length > 0 && $scope.upload.status != 'Unsuccessful') {
        $scope.upload.status = 'Uploading';
        apiService
          .uploadAttachment(
            stateService.access_token,
            stateService.ein,
            fileinput.files[0]
          )
          .then(
            function(result) {
              $scope.upload.status = 'Success';
              $scope.attachmentId = result.data[0].id;
              $scope.attachmentName = result.data[0].originalFileName;
              fileinput.value = '';
            },
            function(error) {
              //TODO: Display error
              fileinput.value = '';
              $scope.upload.status = 'Unsuccessful';
              $scope.upload.message = error.statusMessage;
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
