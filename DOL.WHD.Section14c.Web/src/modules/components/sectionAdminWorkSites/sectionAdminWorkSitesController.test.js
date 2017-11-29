describe('sectionAdminWorkSitesController', function() {
  var scope, $location, route, sectionAdminWorkSitesController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(_$rootScope_, _$controller_, _$location_, $route) {
      scope = _$rootScope_.$new();
      $location = _$location_;
      route = $route;

      sectionAdminWorkSitesController = function() {
        return _$controller_('sectionAdminWorkSitesController', {
          $scope: scope,
          $route: route
        });
      };
    })
  );

  it('should set the active tab', function() {
    var controller = sectionAdminWorkSitesController();
    controller.setActiveTab(2);
    expect(controller.activeTab).toEqual(2);
  });

  it('should reject invalid active tab', function() {
    var controller = sectionAdminWorkSitesController();
    controller.setActiveTab(3);
    expect(controller.activeTab).toEqual(1);
  });

  it('should set item_id when viewing a worksite', function() {
    var controller = sectionAdminWorkSitesController();
    controller.viewWorkSite(1);
    var search = $location.search();
    expect(search.item_id).toEqual(2);
  });

  it('should clear search params when view all worksites is clicked', function() {
    var controller = sectionAdminWorkSitesController();
    controller.viewAllWorkSites();
    var search = $location.search();
    expect(search.item_id).not.toBeDefined();
    expect(search.t).not.toBeDefined();
  });
});
