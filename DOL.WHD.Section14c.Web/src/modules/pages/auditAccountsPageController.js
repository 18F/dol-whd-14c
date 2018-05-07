'use strict';

module.exports = function(ngModule) {
  ngModule.controller('auditAccountsPageController', function(
    $scope,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;

    apiService.getNewAuditAccounts(stateService.access_token).then(
      function(result) {
        var data = result.data;
        $scope.auditAccounts = data;
      },
      function() {
        //vm.loadingError = true;
        $scope.gridOptions.data = [];        
      }
    );     
    
  });
};
