'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminEmployer', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAdminEmployerTemplate.html')
    };
  });
};
