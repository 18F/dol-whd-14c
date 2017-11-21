'use strict';

module.exports = function(ngModule) {
  ngModule.controller('userManagementPageController', function(
    $scope,
    stateService
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;
  });
};
