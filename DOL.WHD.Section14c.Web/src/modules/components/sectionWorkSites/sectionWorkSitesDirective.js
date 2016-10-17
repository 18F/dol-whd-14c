'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionWorkSites', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./sectionWorkSitesTemplate.html'),
          controller: 'sectionWorkSitesController',
          scope: { },
          controllerAs: 'vm'
      };
  });
}
