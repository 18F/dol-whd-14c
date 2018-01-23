describe('dolHeaderController', function() {
  var dolHeaderController, mockIdleService, mockStateService, scope;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller,
      stateService,
      Idle
    ) {
      scope = $rootScope.$new();
      mockIdleService = Idle;
      mockStateService = stateService;
      dolHeaderController = function() {
        return $controller('dolHeaderController', {
          $scope: scope,
          Idle: mockIdleService,
          stateService: mockStateService
        });
      };

      spyOn(mockIdleService, 'watch');
      spyOn(mockStateService, 'logOut');
    })
  );

  it('invoke controller', function() {
    var controller = dolHeaderController();
    expect(controller).toBeDefined();
    expect(scope.$$listenerCount.IdleTimeout).toBeDefined();
    expect(scope.$$listenerCount.IdleWarn).toBeDefined();
  });

  it('starts idle watch on resume', function() {
    var controller = dolHeaderController();
    scope.hideIdleWarning();
    scope.$apply();
    expect(mockIdleService.watch).toHaveBeenCalled();
    expect(scope.modalIsVisible).toEqual(false);
  });

  it('closes idle modal', function() {
    var controller = dolHeaderController();
    scope.hideIdleWarning();
    scope.$apply();
    expect(scope.modalIsVisible).toEqual(false);
  });

  it('shows idle modal', function() {
    var controller = dolHeaderController();
    scope.showIdleWarning();
    scope.$apply();
    expect(scope.modalIsVisible).toEqual(true);
  });

  it('logouts out after timeout is met', function() {
    var controller = dolHeaderController();
    scope.$$listeners.IdleTimeout[0]();
    scope.$apply();
    expect(mockStateService.logOut).toHaveBeenCalled();
    expect(scope.modalIsVisible).toEqual(false);
  });

  it('shows modal for inactivity warning', function() {
    var controller = dolHeaderController();
    scope.$$listeners.IdleWarn[0]();
    scope.$apply();
    expect(scope.modalIsVisible).toEqual(true);
  });

});
