'use strict';

module.exports = function(ngModule) {
  ngModule.controller('dashboardController', function(
    $scope,
    stateService,
    $location
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;

    $scope.changePassword = function() {
      $location.path('/changePassword');
    };

    $scope.navToApplication = function() {
      $location.path('/section/assurances');
    };

  });
};
