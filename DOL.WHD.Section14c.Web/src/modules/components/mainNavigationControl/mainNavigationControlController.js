'use strict';

module.exports = function(ngModule) {
  ngModule.controller('mainNavigationControlController', function(
    $scope,
    $location,
    $route,
    navService,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;
    vm.navService = navService;
    vm.current = $route.current.params.section_id;
    vm.collapseMenu = true; //collapse menu by default for small screens

    this.onNavClick = function(event) {
      var id = event.target.dataset.sectionid;
      navService.gotoSection(id);
    };

    this.onKeyPress = e => {
      if (e.which === 13) this.onNavClick(e);
    };
  });
};
