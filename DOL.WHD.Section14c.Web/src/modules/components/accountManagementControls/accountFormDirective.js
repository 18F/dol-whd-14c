'use strict';

module.exports = function(ngModule) {
  ngModule.directive('accountForm', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./accountFormTemplate.html'),
          controller: 'accountFormController',
          controllerAs: 'vm'
      };
  });
}
