'use strict';

module.exports = function(ngModule) {
  ngModule.controller('changePasswordPageController', function($scope, $location, stateService) {
      'ngInject';
      'use strict';

      $scope.stateService = stateService;
  });
}
