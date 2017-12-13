describe('mainTopNavControlController', function() {
  var scope, mockLocation, mockStateService, mockAutoSaveService, mockNavService;
  var mainTopNavControlController;

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
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    spyOn(mockLocation, 'path');
    controller.userClick(e);
    expect(e.preventDefault).toHaveBeenCalled();
    expect(mockLocation.path).toHaveBeenCalledWith('/changePassword');
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });

  it('save click', function() {
    var controller = mainTopNavControlController();
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    spyOn(mockLocation, 'path');
    spyOn(mockStateService, 'logOut');
    controller.saveClick(e);

    expect(mockStateService.logOut).toHaveBeenCalled();
    expect(mockLocation.path).toHaveBeenCalledWith('/');
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });

  it('dashboard click', function() {
    var controller = mainTopNavControlController();
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    spyOn(mockLocation, 'path');
    controller.dashboardClick(e);

    expect(mockLocation.path).toHaveBeenCalledWith('/');
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });
});
