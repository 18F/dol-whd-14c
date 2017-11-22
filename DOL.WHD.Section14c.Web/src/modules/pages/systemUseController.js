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

    if (stateService.loggedIn) {
      $location.path('/home');
    }

    $scope.navToLanding = function() {
      $location.path('/home');
    };

    $scope.showDetails = false;

    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
      console.log($scope.showDetails);
    }
  });
};
