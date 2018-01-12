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
    $scope.formData = stateService.formData;
    if(!$scope.formData[$scope.modelPrefix][$scope.inputId]) {
      stateService.formData[$scope.modelPrefix][$scope.inputId] = [];
    }
    this.onAttachmentSelected = function(fileinput) {

      if (fileinput.files.length > 0) {

        apiService
          .uploadAttachment(
            stateService.access_token,
            stateService.ein,
            fileinput.files[fileinput.files.length-1]
          )
          .then(
            function(result) {
              var attachment = {};

              attachment.attachmentId = result.data[0].id;
              attachment.attachmentName = result.data[0].originalFileName;
              fileinput.value = '';
              if($scope.inputId === "prScaWageDeterminationAttachments" || $scope.inputId === "hScaWageDeterminationAttachments") {
                $scope.formData[$scope.modelPrefix][$scope.inputId].push(attachment);
              } else {
                if($scope.formData[$scope.modelPrefix][$scope.inputId][0]) {
                  vm.deleteAttachment($scope.formData[$scope.modelPrefix][$scope.inputId][0].attachmentId);
                }
                $scope.formData[$scope.modelPrefix][$scope.inputId][0] = attachment;
              }
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
        .deleteAttachment(stateService.access_token, stateService.ein, id)
        .then(
          function() {
            $scope.formData[$scope.modelPrefix][$scope.inputId].forEach(function(element, index) {
              if (element.attachmentId === id) {
                $scope.formData[$scope.modelPrefix][$scope.inputId][index].attachmentId = undefined;
                $scope.formData[$scope.modelPrefix][$scope.inputId][index].attachmentName = undefined;
              }
            });
          },
          function() {
            //TODO: Display error
          }
        );
    };
  });
};
