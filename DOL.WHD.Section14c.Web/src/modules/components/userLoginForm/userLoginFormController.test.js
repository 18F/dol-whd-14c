describe('userLoginFormController', function() {
  var scope, $q, mockLocation, mockauthService, route;
  var mockstateService, userLoginFormController, userLogin;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller,
      _$q_,
      $route,
      authService,
      stateService,
      $location
    ) {
      scope = $rootScope.$new();
      $q = _$q_;
      route = $route;
      mockLocation = $location;
      mockauthService = authService;
      mockstateService = stateService;
      userLoginFormController = function() {
        return $controller('userLoginFormController', {
          $scope: scope,
          $route: route,
          authService: mockauthService,
          stateService: mockstateService,
          $location: mockLocation
        });
      };

      userLogin = $q.defer();
      spyOn(mockauthService, 'userLogin').and.returnValue(userLogin.promise);
    })
  );

  it('userLoginFormController has clearError', function() {
    var controller = userLoginFormController();
    controller.loginError = true;
    controller.unknownError = true;
    controller.clearError();

    expect(controller.loginError).toBe(false);
    expect(controller.unknownError).toBe(false);
  });

  it('userLoginFormController has forgotPassword', function() {
    userLoginFormController();
    scope.forgotPassword();

    expect(mockLocation.path()).toBe('/forgotPassword');
  });

  it('userLoginFormController has onSubmitClick', function() {
    var controller = userLoginFormController();
    spyOn(controller, 'clearError');
    scope.onSubmitClick();

    expect(controller.submittingForm).toBe(true);
    expect(controller.clearError).toHaveBeenCalled();
  });

  it('on login fails', function() {
    var controller = userLoginFormController();

    scope.onSubmitClick();
    userLogin.reject({ data: {} });
    scope.$apply();

    expect(controller.submittingForm).toBe(false);
  });

  it('on login fails with pasword expired', function() {
    userLoginFormController();
    spyOn(scope, '$apply');
    scope.onSubmitClick();
    userLogin.reject({ data: { error_description: 'Password expired' } });
    scope.$digest();

    expect(mockstateService.user.passwordExpired).toBe(true);
    expect(mockLocation.path()).toBe('/changePassword');
  });

  it('on login fails with 400', function() {
    var controller = userLoginFormController();

    scope.onSubmitClick();
    userLogin.reject({ status: 400 });
    scope.$apply();

    expect(controller.loginError).toBe(true);
  });

  it('on user info success', function() {
    userLoginFormController();
    scope.onSubmitClick();
    userLogin.resolve({ data: {} });
    scope.$apply();

    expect(mockauthService.userLogin).toHaveBeenCalled();
  });

  it('toggle hideShowPassword should show password if it is hidden', function() {
    userLoginFormController();
    scope.inputType = 'password';
    scope.hideShowPassword();
    scope.$apply();

    expect(scope.inputType).toBe('text');
  });

  it('toggle hideShowPassword should hide password if it is shown', function() {
    userLoginFormController();
    scope.inputType = 'text';
    scope.hideShowPassword();
    scope.$apply();

    expect(scope.inputType).toBe('password');
  });
});
