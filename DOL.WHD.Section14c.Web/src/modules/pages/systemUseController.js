'use strict';

module.exports = function(ngModule) {
  ngModule.controller('systemUseController', function(
    $scope,
    stateService,
    $location
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;

    // redirect to dashboar (home) if user is logged in
    if (stateService.loggedIn) {
      $location.path('/login');
    }

    $scope.navToLanding = function() {
      $location.path('/login');
    };

    $scope.showDetails = false;

    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
      log.info($scope.showDetails);
    }
  });
};