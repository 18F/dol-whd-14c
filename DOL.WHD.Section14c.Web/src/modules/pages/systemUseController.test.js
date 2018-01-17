describe('systemUseController', function() {
  var scope, systemUseController, organizations, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, stateService, _$q_, $location) {
      scope = $rootScope.$new();
      mockStateService = stateService;
      $q = _$q_;
      location = $location

      systemUseController = function() {
        return $controller('systemUseController', {
          $scope: scope,
          stateService: mockStateService
        });
      };
      controller = systemUseController();
    })
  );

  it('invoke controller', function() {
    expect(controller).toBeDefined();
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });
});
