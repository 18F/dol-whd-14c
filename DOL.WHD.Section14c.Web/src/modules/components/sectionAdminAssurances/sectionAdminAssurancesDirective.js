'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminAssurances', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAdminAssurancesTemplate.html')
    };
  });
};
