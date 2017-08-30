describe('resetPasswordFormController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller,
      _$q_,
      $location,
      apiService,
      stateService
    ) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;
      mockStateService = { user: {} };
      nockLocation = $location;

      resetPasswordFormController = function() {
        return $controller('resetPasswordFormController', {
          $scope: scope,
          apiService: mockApiService,
          stateService: mockStateService,
          $location: nockLocation
        });
      };

      resetPassword = $q.defer();
      spyOn(mockApiService, 'resetPassword').and.returnValue(
        resetPassword.promise
      );

      verifyResetPassword = $q.defer();
      spyOn(mockApiService, 'verifyResetPassword').and.returnValue(
        verifyResetPassword.promise
      );
    })
  );

  it('reset passwork onSubmitClick success should show success message', function() {
    var controller = resetPasswordFormController();
    scope.onSubmitClick();
    resetPassword.resolve({});
    scope.$apply();

    expect(controller.forgotPasswordSuccess).toBe(true);
  });

  it('reset passwork onSubmitClick error should show error message, log details', function() {
    var controller = resetPasswordFormController();
    scope.onSubmitClick();
    resetPassword.reject({ data: { error: 'test' } });
    scope.$apply();

    expect(controller.forgotPasswordError).toBe(true);
  });

  it('toggle hideShowPassword should show password if it is hidden', function() {
    var controller = resetPasswordFormController();
    scope.inputType = 'password';
    scope.hideShowPassword();
    scope.$apply();

    expect(scope.inputType).toBe('text');
  });

  it('toggle hideShowPassword should hide password if it is shown', function() {
    var controller = resetPasswordFormController();
    scope.inputType = 'text';
    scope.hideShowPassword();
    scope.$apply();

    expect(scope.inputType).toBe('password');
  });

  it('verify password reset should show success message and reset form values', function() {
    var controller = resetPasswordFormController();

    scope.formVals.newPass = 'passwordVal';
    scope.formVals.confirmPass = 'passwordVal';
    scope.onVerifySubmitClick();
    verifyResetPassword.resolve({});
    scope.$apply();

    expect(controller.resetPasswordSuccess).toBe(true);
    expect(scope.formVals.newPass).toBe('');
    expect(scope.formVals.confirmPass).toBe('');
  });

  it('verify password reset error should show error and return errors, password fields not to be reset', function() {
    var controller = resetPasswordFormController();

    scope.formVals.newPass = 'passwordVal';
    scope.formVals.confirmPass = 'passwordVal';
    scope.onVerifySubmitClick();
    verifyResetPassword.reject({
      data: { modelState: { error: ['message'] } }
    });
    scope.$apply();

    expect(controller.resetPasswordError).toBe(true);
    expect(scope.resetPasswordErrors[0]).toBe('message');
    expect(scope.formVals.newPass).toBe('passwordVal');
    expect(scope.formVals.confirmPass).toBe('passwordVal');
  });

  it('verify password reset error should show error and return errors, log error details', function() {
    var controller = resetPasswordFormController();
    scope.onVerifySubmitClick();
    verifyResetPassword.reject({ data: { error: 'test' } });
    scope.$apply();

    expect(controller.resetPasswordError).toBe(true);
  });

  it('show verify password form when code and userId are present', function() {
    spyOn(nockLocation, 'search').and.returnValue({
      code: 'code',
      userId: 'userId'
    });

    var controller = resetPasswordFormController();
    scope.$apply();

    expect(controller.isResetPasswordVerificationRequest).toBe(true);
  });

  it('password strength, length met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = '12345678';
    scope.$apply();

    expect(controller.passwordLength).toBe(true);
  });

  it('password strength, length not met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = '1234567';
    scope.$apply();

    expect(controller.passwordLength).toBe(false);
  });

  it('password strength, upper met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = 'P';
    scope.$apply();

    expect(controller.passwordUpper).toBe(true);
  });

  it('password strength, upper not met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = 'p';
    scope.$apply();

    expect(controller.passwordUpper).toBe(false);
  });

  it('password strength, lower met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = 'p';
    scope.$apply();

    expect(controller.passwordLower).toBe(true);
  });

  it('password strength, lower not met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = 'P';
    scope.$apply();

    expect(controller.passwordLower).toBe(false);
  });

  it('password strength, special met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = '%';
    scope.$apply();

    expect(controller.passwordSpecial).toBe(true);
  });

  it('password strength, special not met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = 'p';
    scope.$apply();

    expect(controller.passwordSpecial).toBe(false);
  });

  it('password strength, number met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = '1';
    scope.$apply();

    expect(controller.passwordNumber).toBe(true);
  });

  it('password strength, number not met', function() {
    var controller = resetPasswordFormController();
    scope.formVals.newPass = 'p';
    scope.$apply();

    expect(controller.passwordNumber).toBe(false);
  });
});
