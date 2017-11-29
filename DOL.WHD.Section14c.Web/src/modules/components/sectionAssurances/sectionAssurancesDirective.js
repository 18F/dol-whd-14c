'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAssurances', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./sectionAssurancesTemplate.html'),
      controller: 'sectionAssurancesController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
