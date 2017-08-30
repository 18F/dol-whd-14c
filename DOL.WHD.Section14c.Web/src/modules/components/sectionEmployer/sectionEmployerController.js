'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionEmployerController', function(
    $scope,
    stateService,
    apiService,
    responsesService,
    validationService,
    _constants
  ) {
    'ngInject';
    'use strict';

    $scope.formData = stateService.formData;
    $scope.validate = validationService.getValidationErrors;

    if (!$scope.formData.employer) {
      $scope.formData.employer = {};
    }

    if (!$scope.formData.employer.numSubminimalWageWorkers) {
      $scope.formData.employer.numSubminimalWageWorkers = {};
    }

    if (!$scope.formData.employer.providingFacilitiesDeductionTypeId) {
      $scope.formData.employer.providingFacilitiesDeductionTypeId = [];
    }

    // multiple choice responses
    let questionKeys = [
      'EmployerStatus',
      'SCA',
      'EO13658',
      'ProvidingFacilitiesDeductionType'
    ];
    responsesService.getQuestionResponses(questionKeys).then(responses => {
      $scope.responses = responses;
    });

    var vm = this;
    vm.showAllHelp = false;

    this.onHasTradeNameChange = function() {
      $scope.formData.employer.tradeName = '';
    };

    this.onHasLegalNameChange = function() {
      $scope.formData.employer.priorLegalName = '';
    };

    this.toggleDeductionType = function(id) {
      let index = $scope.formData.employer.providingFacilitiesDeductionTypeId.indexOf(
        id
      );
      if (index > -1) {
        $scope.formData.employer.providingFacilitiesDeductionTypeId.splice(
          index,
          1
        );
      } else {
        $scope.formData.employer.providingFacilitiesDeductionTypeId.push(id);
      }
    };

    this.notInitialApp = function() {
      return (
        $scope.formData.applicationTypeId !==
        _constants.responses.applicationType.initial
      );
    };
  });
};
