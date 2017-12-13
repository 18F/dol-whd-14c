'use strict';

module.exports = function(ngModule) {
  ngModule.controller('mainTopNavControlController', function(
    $scope,
    $location,
    stateService,
    autoSaveService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;

    this.dashboardClick = e => {
      e.preventDefault();
      $location.path('/dashboard');
    };

    this.userClick = e => {
      e.preventDefault();
      $location.path('/changePassword');
      document.title = 'DOL WHD Section 14(c)';
    };

    this.saveClick = e => {
      e.preventDefault();
      autoSaveService.save(() => {
        stateService.logOut();
        $location.path('/login');
      });
      document.title = 'DOL WHD Section 14(c)';
    };
  });
};
