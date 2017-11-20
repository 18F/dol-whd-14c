'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionAssurancesController', function(
    $scope,
    stateService,
    validationService
  ) {
    'ngInject';
    'use strict';

    $scope.formData = stateService.formData;
    $scope.validate = validationService.getValidationErrors;

    var vm = this;


  });
};
