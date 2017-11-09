'use strict';

module.exports = function(ngModule) {
  ngModule.directive('dolHeader', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./dolHeaderTemplate.html'),
      controller: 'dolHeaderController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
