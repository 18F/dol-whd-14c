'use strict';

module.exports = function(ngModule) {
  ngModule.directive('userRegistrationForm', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./userRegistrationFormTemplate.html'),
      controller: 'userRegistrationFormController',
      controllerAs: 'vm'
    };
  });
};
