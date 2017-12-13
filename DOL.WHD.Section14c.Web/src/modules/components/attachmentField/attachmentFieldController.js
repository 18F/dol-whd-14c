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
      if (fileinput.files.length > 0) {
        apiService
          .uploadAttachment(
            stateService.access_token,
            stateService.applicationId,
            fileinput.files[0]
          )
          .then(
            function(result) {
              $scope.attachmentId = result.data[0].id;
              $scope.attachmentName = result.data[0].originalFileName;
              fileinput.value = '';
            },
            function() {
              //TODO: Display error
              fileinput.value = '';
            }
          );
      }
    };

    this.deleteAttachment = function(id) {
      apiService
        .deleteAttachment(stateService.access_token, stateService.applicationId, id)
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
