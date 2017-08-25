'use strict';

module.exports = function(ngModule) {
  ngModule.directive('changePasswordForm', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./changePasswordFormTemplate.html'),
          controller: 'changePasswordFormController',
          controllerAs: 'vm'
      };
  });
}
