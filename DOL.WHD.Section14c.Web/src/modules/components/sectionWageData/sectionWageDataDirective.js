'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionWageData', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./sectionWageDataTemplate.html'),
          controller: 'sectionWageDataController',
          controllerAs: 'section'
      };
  });
}
