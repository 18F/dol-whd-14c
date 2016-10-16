'use strict';

module.exports = function(ngModule) {
  ngModule.directive('mainHeaderControl', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./mainHeaderControlTemplate.html'),
          controller: 'mainHeaderControlController',
          scope: {},
          controllerAs: 'vm'
      };
  });
}
