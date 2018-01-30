describe('systemUseController', function() {
  var scope, systemUseController, organizations, mockLocation, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, stateService, $location, _$q_, $location) {
      scope = $rootScope.$new();
      mockStateService = stateService;
      mockLocation = $location;
      $q = _$q_;
      location = $location

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
    console.log(scope.toggleDetails)
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });

  it('has function to toggle details', function() {
    expect(scope.showDetails).toBe(false);
    expect(scope.toggleDetails).toBeDefined();
    scope.toggleDetails();
    scope.$apply();
    expect(scope.showDetails).toBe(true);
  });

  it('has function to toggle details', function() {
    spyOn(mockLocation, 'path')
    scope.navToLanding();
    scope.$apply();
    expect(mockLocation.path).toHaveBeenCalledWith('/login');
  });
});
