describe('sectionWageDataController', function() {
  var scope, route, sectionWageDataController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route) {
      scope = $rootScope.$new();
      route = $route;

      sectionWageDataController = function() {
        return $controller('sectionWageDataController', {
          $scope: scope,
          $route: route
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = sectionWageDataController();
    spyOn(controller, 'setNextTabQuery');
    controller.onTabClick(1);

    expect(controller.activeTab).toBe(1);
    expect(controller.setNextTabQuery).toHaveBeenCalledWith(1);
  });

  //TODO test rest of controller
});
