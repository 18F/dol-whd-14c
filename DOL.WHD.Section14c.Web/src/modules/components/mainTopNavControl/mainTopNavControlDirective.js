'use strict';

module.exports = function(ngModule) {
  ngModule.directive('mainTopNavControl', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./mainTopNavControlTemplate.html'),
          controller: 'mainTopNavControlController',
          scope: {
              admin: '='
          },
          controllerAs: 'vm'
      };
  });
}
