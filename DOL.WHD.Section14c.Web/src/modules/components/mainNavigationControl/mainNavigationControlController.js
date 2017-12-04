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
        element.ariaLabel = element.display;
      }
      return element;
    });
    vm.stateService = stateService;
    vm.navService = navService;
    vm.current = $location.$$path.split('/section/')[1];
    vm.collapseMenu = true; //collapse menu by default for small screens
    this.onNavClick = function(event) {
      function toTitleCase(str) {
<<<<<<< HEAD
      return str.replace(/\w\S*/g, function (txt) {
          return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
=======
          return str.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
>>>>>>> Formatted titles
      }

      var id = event.target.dataset.sectionid;
      navService.gotoSection(id);
<<<<<<< HEAD
    

    if (id === 'app-info') {
      document.title = 'Application Info | DOL WHD Section 14(c)';
    } else if (id === 'work-sites') {
      document.title = 'Work Sites & Employees | DOL WHD Section 14(c)';
    } else if (id === 'wioa') {
      document.title = 'WIOA | DOL WHD Section 14(c)';
    } else {
      id = id.replace(/-/g, ' ');
      document.title = toTitleCase(id) + ' | DOL WHD Section 14(c)';
    }
=======
>>>>>>> Formatted titles

      if(id === "app-info") {
        document.title = "Application Info | DOL WHD Section 14(c)";
      } else if (id === "work-sites") {
        document.title = "Work Sites & Employees | DOL WHD Section 14(c)";
      } else if (id === "wioa") {
        document.title = "WIOA | DOL WHD Section 14(c)";
      } else {
        id = id.replace(/-/g, ' ');
        document.title = toTitleCase(id) + " | DOL WHD Section 14(c)";
      }
    };
    
    this.onKeyPress = e => {
      if (e.which === 13) this.onNavClick(e);
    };
  };
  });
};
