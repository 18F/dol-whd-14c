'use strict';

module.exports = function(ngModule) {
  ngModule.controller('adminDashboardController', function($scope, $location, stateService) {
      'ngInject';
      'use strict';

      $scope.appList = stateService.appList;

      $scope.gotoUsers = function() {
          $location.path("/admin/users");
      }
  });
}
