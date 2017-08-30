'use strict';

module.exports = function(ngModule) {
  ngModule.directive('accountGrid', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./accountGridTemplate.html'),
      controller: 'accountGridController',
      controllerAs: 'vm'
    };
  });
};
