'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminWageData', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./sectionAdminWageDataTemplate.html'),
          controller: 'sectionAdminWageDataController',
          controllerAs: 'vm'
      };
  });
}
