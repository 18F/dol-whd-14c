'use strict';

module.exports = function(ngModule) {
  ngModule.directive('accountCreateButton', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./accountCreateButtonTemplate.html'),
      controller: 'accountCreateButtonController',
      controllerAs: 'vm'
    };
  });
};
