'use strict';

module.exports = function(ngModule) {
  ngModule.controller('mainHeaderControlController', function(
    $scope,
    assetService,
    $location,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    $scope.appData = stateService.appData;

    var vm = this;
    vm.stateService = stateService;
    vm.assetService = assetService;
    vm.organizations = [];
    apiService.userInfo(vm.stateService.access_token).then(function(result){
      vm.organizations = result.data.organizations;
      vm.organization = vm.organizations.filter(function(element) {
        return element.ein === stateService.ein;
      })[0];
    });
  });
};
