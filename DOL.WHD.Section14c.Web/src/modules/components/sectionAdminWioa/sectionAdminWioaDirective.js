'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminWioa', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAdminWioaTemplate.html')
    };
  });
};
