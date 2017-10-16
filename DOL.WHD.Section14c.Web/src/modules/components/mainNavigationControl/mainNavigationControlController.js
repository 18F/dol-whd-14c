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
    vm.sections = navService.getSections().map(function(element){
      console.log(element);
      if(element.display === "WIOA") {
        element.ariaLabel = "Workforce Innovation and Opportunity Act"
      } else {
        element.ariaLabel = element.display;
      }
      return element;
    });
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

    this.getSections = function() {
      var sections = vm.navService.getSections();
      sections = sections.map(function(element){
        if(element.display === "WOIA") {
          element.ariaLabel = "Workforce Innovation and Opportunity Act"
        } else {
          element.ariaLabel = element.display;
        }
        return element;
      });

      return sections;
    }

  });
};
