'use strict';

module.exports = function(ngModule) {
  ngModule.directive('auditAccounts', function() {
    'use strict';

    return {
      transclude: true,
      template: require('./auditAccountsTemplate.html'),
      replace: true,
      scope: {
        answer: '=',
        addressField: '=',
        datefield: '=',
        attachmentField: '='
      },
      controller: 'auditAccountsController',
      controllerAs: 'vm'
    };
  });
};
