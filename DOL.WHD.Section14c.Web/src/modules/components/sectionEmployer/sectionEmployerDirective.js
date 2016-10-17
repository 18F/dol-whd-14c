'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionEmployer', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./sectionEmployerTemplate.html'),
          controller: 'sectionEmployerController',
          scope: { },
          controllerAs: 'vm'
      };
  });
}
