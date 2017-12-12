'use strict';

module.exports = function(ngModule) {
  ngModule.controller('homePageController', function(
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
