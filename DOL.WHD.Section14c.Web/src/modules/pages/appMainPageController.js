'use strict';

module.exports = function(ngModule) {
  angular.module('app').controller('appMainPageController', function($scope) {
      'ngInject';
      'use strict';

      var initialize = function() {
          $scope.worldGreeting = 'Hello';
      };

      initialize();
  });
}
