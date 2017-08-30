'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminAppInfo', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAdminAppInfoTemplate.html')
    };
  });
};
