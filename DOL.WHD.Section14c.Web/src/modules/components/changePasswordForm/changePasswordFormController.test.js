describe('changePasswordFormController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService, stateService) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;
      mockStateService = { user: {} };

      changePasswordFormController = function() {
        return $controller('changePasswordFormController', {
          $scope: scope,
          apiService: mockApiService,
          stateService: mockStateService
        });
      };

      changePassword = $q.defer();
      spyOn(mockApiService, 'changePassword').and.returnValue(
        changePassword.promise
      );
    })
  );

  it('invoke controller', function() {
    var controller = changePasswordFormController();
  });

  it('change password click success, reset form and display success message', function() {
    var controller = changePasswordFormController();
    scope.onSubmitClick();
    changePassword.resolve({});
    scope.$apply();

    expect(mockStateService.user.loginEmail).toBe('');
    expect(scope.formVals.currentPass).toBe('');
    expect(scope.formVals.newPass).toBe('');
    expect(scope.formVals.confirmPass).toBe('');
    expect(controller.changePasswordSuccess).toBe(true);
  });

  it('change password click error, parse and show errors', function() {
    var controller = changePasswordFormController();
    scope.onSubmitClick();
    changePassword.reject({ data: { error: 'test' } });
    scope.$apply();
    expect(controller.changePasswordError).toBe(true);
  });

  it('change password click error, parse and show errors no details', function() {
    var controller = changePasswordFormController();
    scope.onSubmitClick();
    changePassword.reject({ data: {} });
    scope.$apply();
    expect(controller.changePasswordError).toBe(true);
  });
});
