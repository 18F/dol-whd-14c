describe('mainNavigationControlController', function() {
  var scope, route, mockNavService;
  var mainNavigationControlController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route, navService) {
      scope = $rootScope.$new();
      route = $route;
      mockNavService = navService;

      spyOn(mockNavService, 'hasNext');
      spyOn(mockNavService, 'hasBack');

      route.current = {
        params: { section_id: 1 }
      };

      spyOn(mockNavService, 'getNextSection');

      mainNavigationControlController = function() {
        return $controller('mainNavigationControlController', {
          $scope: scope,
          navService: mockNavService,
          $route: route
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = mainNavigationControlController();
    spyOn(mockNavService, 'gotoSection');
    var event = { target: { dataset: { sectionid: 1 } } };
    controller.onNavClick(event);

    expect(mockNavService.gotoSection).toHaveBeenCalledWith(1);
  });
});
