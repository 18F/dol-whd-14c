'use strict';

module.exports = function(ngModule) {
  ngModule.directive('userLogin', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./userLoginFormTemplate.html'),
      controller: 'userLoginFormController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
