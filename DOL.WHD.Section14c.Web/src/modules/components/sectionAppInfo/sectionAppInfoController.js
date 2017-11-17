'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionAppInfoController', function(
    $scope,
    crumble,
    $location,
    stateService,
    responsesService,
    validationService
  ) {
    'ngInject';
    'use strict';

    $scope.formData = stateService.formData;
    $scope.validate = validationService.getValidationErrors;
    $scope.showAllHelp = false;

    if (!$scope.formData.establishmentTypeId) {
      $scope.formData.establishmentTypeId = [];
    }

    // multiple choice responses
    let questionKeys = ['ApplicationType', 'EstablishmentType'];
    responsesService.getQuestionResponses(questionKeys).then(responses => {
      $scope.responses = responses;
    });

    let vm = this;

    this.toggleEstablishmentType = function(id) {
      let index = $scope.formData.establishmentTypeId.indexOf(id);
      if (index > -1) {
        $scope.formData.establishmentTypeId.splice(index, 1);
      } else {
        $scope.formData.establishmentTypeId.push(id);
      }
    };

    this.toggleAllHelpText = function() {
      $scope.showAllHelp = !$scope.showAllHelp;
    }
  });
};
