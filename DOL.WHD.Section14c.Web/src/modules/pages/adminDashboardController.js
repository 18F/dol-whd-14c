'use strict';

module.exports = function(ngModule) {
  ngModule.controller('adminDashboardController', function($scope) {
      'ngInject';
      'use strict';

      $scope.answerVal = "This is the answer";
  });
}
