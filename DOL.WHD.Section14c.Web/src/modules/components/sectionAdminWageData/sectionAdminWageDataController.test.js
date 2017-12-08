describe('sectionAdminWageDataController', function() {
  var scope, locationMock;
  var sectionAdminWageDataController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $location) {
      scope = $rootScope.$new();
      locationMock = $location;

      sectionAdminWageDataController = function() {
        return $controller('sectionAdminWageDataController', {
          $scope: scope,
          $location: locationMock,
          _constants: { }
        });
      };
    })
  );

  it('constructor', function() {
    spyOn(locationMock, 'search').and.returnValue({ });
    sectionAdminWageDataController();

    expect(locationMock.search).toHaveBeenCalled();
  });

  it('onTabClick sets the active tab', function() {
    var controller = sectionAdminWageDataController();
    controller.onTabClick('active tab id');

    expect(controller.activeTab).toBe('active tab id');
  });

  describe('$routeUpdate event handler', function() {
    it('sets the active tab to the default when the location search does not define one', function() {
      spyOn(locationMock, 'search').and.returnValue({ });
      var controller = sectionAdminWageDataController();
      controller.activeTab = 0;

      scope.$broadcast('$routeUpdate');

      expect(locationMock.search).toHaveBeenCalled();
      expect(controller.activeTab).toBe(1);
    });

    it('sets the active tab to the value defined in location search', function() {
      spyOn(locationMock, 'search').and.returnValue({ t: 'active tab id' });
      var controller = sectionAdminWageDataController();
      controller.activeTab = 0;

      scope.$broadcast('$routeUpdate');

      expect(locationMock.search).toHaveBeenCalled();
      expect(controller.activeTab).toBe('active tab id');
    });
  });
});
