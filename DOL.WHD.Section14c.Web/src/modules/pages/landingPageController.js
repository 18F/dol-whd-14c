'use strict';

module.exports = function(ngModule) {
  ngModule.controller('landingPageController', function(
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
  });
};
