'use strict';

module.exports = function(ngModule) {
  ngModule.directive('stateField', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./stateFieldTemplate.html'),
          controller: 'stateFieldController',
          scope: {
              selectedState: '=',
              name: '@'
          },
          controllerAs: 'vm'
      };
  });
}
