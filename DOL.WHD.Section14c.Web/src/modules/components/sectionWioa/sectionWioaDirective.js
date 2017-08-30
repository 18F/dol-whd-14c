'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionWioa', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionWioaTemplate.html'),
      controller: 'sectionWioaController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
