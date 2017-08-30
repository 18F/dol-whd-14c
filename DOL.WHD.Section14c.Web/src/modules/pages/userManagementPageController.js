'use strict';

module.exports = function(ngModule) {
  ngModule.controller('userManagementPageController', function(
    $scope,
    stateService,
    $location
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;
  });
};
