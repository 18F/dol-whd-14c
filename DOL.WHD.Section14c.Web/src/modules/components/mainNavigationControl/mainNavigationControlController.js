'use strict';

module.exports = function(ngModule) {
  ngModule.controller('mainNavigationControlController', function(
    $scope,
    $location,
    $route,
    navService,
    stateService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.sections = navService.getSections().map(function(element){
      if(element.display === "WIOA") {
        element.ariaLabel = "Workforce Innovation and Opportunity Act"
      } else {
        document.title = element.display;
        element.ariaLabel = element.display;
      }
      return element;
    });
    vm.stateService = stateService;
    vm.navService = navService;
    vm.current = $location.$$path.split('/section/')[1];
    vm.collapseMenu = true; //collapse menu by default for small screens
    this.onNavClick = function(event) {
      var id = event.target.dataset.sectionid;
      document.title = id + " | DOL WHD Sectionn 14(c)";
      navService.gotoSection(id);
    };

    this.onKeyPress = e => {
      if (e.which === 13) this.onNavClick(e);
    };

  });
};
