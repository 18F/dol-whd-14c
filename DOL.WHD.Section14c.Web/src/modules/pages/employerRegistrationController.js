'use strict';

module.exports = function(ngModule) {
  ngModule.controller('employerRegistrationController', function(
    $scope,
    stateService,
    $location
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.restForm = function() {
      $scope.formVals = {
        hasTradeName: '',
        legalName: '',
        ein: '',
        physicalAddress: {
          streetAddress: '',
          city: '',
          state: '',
          zipCode: '',
          county: ''
        }
      };
    };
    $scope.stateService = stateService;


    $scope.navToLanding = function() {
      $location.path('/home');
    };

    $scope.showDetails = false;

    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
    }

    $scope.onSubmitClick = function () {

      apiService.submitEmployer(
        stateService.access_token,

      )
    }

  });
};
