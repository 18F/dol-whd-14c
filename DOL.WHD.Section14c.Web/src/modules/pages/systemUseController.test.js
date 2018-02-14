describe('systemUseController', function() {
  var scope, systemUseController, mockLocation, mockStateService, controller;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, stateService, $location) {
      scope = $rootScope.$new();
      mockStateService = stateService;
      mockLocation = $location;

      systemUseController = function() {
        return $controller('systemUseController', {
          $scope: scope,
          stateService: mockStateService,
          $location: mockLocation
        });
      };
      controller = systemUseController();
    })
  );

  it('invoke controller', function() {
    expect(controller).toBeDefined();
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });

  it('has function to toggle details', function() {
    expect(scope.showDetails).toBe(false);
    expect(scope.toggleDetails).toBeDefined();
    scope.toggleDetails();
    scope.$apply();
    expect(scope.showDetails).toBe(true);
  });

  it('navigates to login', function() {
    spyOn(mockLocation, 'path')
    scope.navToLanding();
    scope.$apply();
    expect(mockLocation.path).toHaveBeenCalledWith('/login');
  });
});
