/*
global jasmine
*/

describe('mainTopNavControlController', function() {
  var scope, mockLocation, mockStateService, mockAutoSaveService;
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
  });

  it('save click', function() {
    var controller = mainTopNavControlController();
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    spyOn(mockLocation, 'path');
    spyOn(mockStateService, 'logOut');
    controller.saveClick(e);

    expect(mockStateService.logOut).toHaveBeenCalled();
    expect(mockLocation.path).toHaveBeenCalledWith('/');
  });

  it('dashboard click', function() {
    var controller = mainTopNavControlController();
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    spyOn(mockLocation, 'path');
    controller.dashboardClick(e);

    expect(mockLocation.path).toHaveBeenCalledWith('/');
  });
});
