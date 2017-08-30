'use strict';

module.exports = function(ngModule) {
  ngModule.directive('stateField', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./stateFieldTemplate.html'),
      scope: {
        selectedState: '=',
        name: '@'
      }
    };
  });
};
