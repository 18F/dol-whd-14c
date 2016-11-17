'use strict';

module.exports = function(ngModule) {
  ngModule.directive('mainNavigationControl', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./mainNavigationControlTemplate.html'),
          controller: 'mainNavigationControlController',
          scope: {
              admin: '='
          },
          controllerAs: 'vm'
      };
  });
}
