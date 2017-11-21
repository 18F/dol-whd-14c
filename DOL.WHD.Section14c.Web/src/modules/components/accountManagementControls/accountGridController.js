'use strict';

module.exports = function(ngModule) {
  ngModule.controller('accountGridController', function(
    $scope,
    $location,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;
    vm.loadingError = false;

    apiService.getAccounts(stateService.access_token).then(
      function(result) {
        var data = result.data;
        $scope.accounts = data;
      },
      function(error) {
        vm.loadingError = true;
      }
    );

    vm.editAccountClick = function(userId) {
      $location.path('/account/' + userId);
    };
  });
};
