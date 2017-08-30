'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAppInfo', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAppInfoTemplate.html'),
      controller: 'sectionAppInfoController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
