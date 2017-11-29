'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionAdminWorkSitesController', function(
    $location,
    $route
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.activeTab = 1;

    this.siteRowClicked = e => {
      let row = $(e.target).closest('.expanding-row');
      row.toggleClass('expanded');
      row.next().toggleClass('show');
    };

    this.viewWorkSite = index => {
      $location.search('item_id', index + 1);
      $route.reload(); // manually reload the route since reloadOnSearch is false
    };

    this.viewAllWorkSites = () => {
      $location.search('item_id', null);
      $location.search('t', null);
      $route.reload(); // manually reload the route since reloadOnSearch is false
    };

    this.setActiveTab = function(tab) {
      if (tab < 1 || tab > 2) {
        return;
      }

      vm.activeTab = tab;
    };
  });
};
