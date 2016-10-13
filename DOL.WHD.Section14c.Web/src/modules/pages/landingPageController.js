'use strict';

module.exports = function(ngModule) {
  ngModule.controller('landingPageController', function($scope, stateService) {
      'ngInject';
      'use strict';

      $scope.stateService = stateService;
  });
}
