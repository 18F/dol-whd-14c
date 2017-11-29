'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminSummary', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAdminSummaryTemplate.html'),
      controller: 'sectionAdminSummaryController',
      controllerAs: 'vm'
    };
  });
};
