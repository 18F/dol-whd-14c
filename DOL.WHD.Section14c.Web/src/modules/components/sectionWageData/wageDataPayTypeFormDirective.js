'use strict';

module.exports = function(ngModule) {
  ngModule.directive('wageDataPayTypeForm', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./wageDataPayTypeFormTemplate.html'),
          controller: 'wageDataPayTypeFormController',
          scope: {
              paytype: '@',
              showAllHelp: '='
          },
          controllerAs: 'vm'
      };
  });
}
