'use strict';

module.exports = function(ngModule) {
  ngModule.controller('employerRegistrationController', function(
    $scope,
    stateService,
    $location
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;

    $scope.navToLanding = function() {
      $location.path('/home');
    };

    $scope.showDetails = false;

    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
      log.info($scope.showDetails);
    }
  });
};
