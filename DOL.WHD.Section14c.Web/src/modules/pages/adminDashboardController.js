'use strict';

module.exports = function(ngModule) {
  ngModule.controller('adminDashboardController', function($scope, stateService) {
      'ngInject';
      'use strict';

      $scope.appList = stateService.appList;
  });
}
