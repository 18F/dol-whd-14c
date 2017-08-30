describe('mainTopNavControlController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $location,
      $rootScope,
      $controller,
      autoSaveService,
      stateService
    ) {
      scope = $rootScope.$new();
      mockLocation = $location;
      mockStateService = stateService;
      mockAutoSaveService = {
        save: function(callback) {
          callback();
        }
      };

      mainTopNavControlController = function() {
        return $controller('mainTopNavControlController', {
          $scope: scope,
          $location: mockLocation,
          autoSaveService: mockAutoSaveService,
          stateService: mockStateService
        });
      };
    })
  );

  it('user click', function() {
    var controller = mainTopNavControlController();
    spyOn(mockLocation, 'path');
    controller.userClick();

    expect(mockLocation.path).toHaveBeenCalled();
  });

  it('save click', function() {
    var controller = mainTopNavControlController();
    spyOn(mockLocation, 'path');
    spyOn(mockStateService, 'logOut');
    controller.saveClick();

    expect(mockStateService.logOut).toHaveBeenCalled();
    expect(mockLocation.path).toHaveBeenCalledWith('/');
  });

  it('dashboard click', function() {
    var controller = mainTopNavControlController();
    spyOn(mockLocation, 'path');
    controller.dashboardClick();

    expect(mockLocation.path).toHaveBeenCalledWith('/');
  });
});
