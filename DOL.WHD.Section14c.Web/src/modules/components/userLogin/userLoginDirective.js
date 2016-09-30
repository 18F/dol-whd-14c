'use strict';

module.exports = function(ngModule) {
  ngModule.directive('userLogin', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./userLoginTemplate.html'),
          controller: 'userLoginController',
          controllerAs: 'vm'
      };
  });
}
