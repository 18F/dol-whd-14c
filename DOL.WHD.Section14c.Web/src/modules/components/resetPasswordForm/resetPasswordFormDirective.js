'use strict';

module.exports = function(ngModule) {
  ngModule.directive('resetPasswordForm', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./resetPasswordFormTemplate.html'),
          controller: 'resetPasswordFormController',
          controllerAs: 'vm'
      };
  });
}
