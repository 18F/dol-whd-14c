describe('userRegistrationFormController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller,
      _$q_,
      apiService,
      vcRecaptchaService,
      $location
    ) {
      $q = _$q_;
      scope = $rootScope.$new();
      mockapiService = apiService;
      mockvcRecaptchaService = vcRecaptchaService;
      mockLocation = $location;

      userRegistrationFormController = function() {
        return $controller('userRegistrationFormController', {
          $scope: scope,
          apiService: mockapiService,
          vcRecaptchaService: mockvcRecaptchaService,
          $location: mockLocation
        });
      };

      userRegister = $q.defer();
      spyOn(mockapiService, 'userRegister').and.returnValue(
        userRegister.promise
      );

      emailVerification = $q.defer();
      spyOn(mockapiService, 'emailVerification').and.returnValue(
        emailVerification.promise
      );
    })
  );

  it('invoke controller', function() {
    var controller = userRegistrationFormController();
  });

  it('userRegistrationFormController has hideShowPassword', function() {
    var controller = userRegistrationFormController();
    scope.hideShowPassword();
  });
  it('userRegistrationFormController has setRegResponse', function() {
    var controller = userRegistrationFormController();
    scope.setRegResponse();
  });
  it('userRegistrationFormController has createRegWidget', function() {
    var controller = userRegistrationFormController();
    scope.createRegWidget();
  });
  it('userRegistrationFormController has hideShowPassword', function() {
    var controller = userRegistrationFormController();
    scope.hideShowPassword();
  });

  it('userRegistrationFormController has hideShowPassword', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.$apply();
  });

  it('submitting registration is successful, email is set as registered and created, windows scrolls to top', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    expect(controller.submittingForm).toBe(true);
    userRegister.resolve();
    scope.$apply();
    expect(controller.submittingForm).toBe(false);
  });

  it('submitting registration has an error, no error data', function() {
    var controller = userRegistrationFormController();
    var hasBeenCalled = false;
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {
      hasBeenCalled = true;
    };
    expect(controller.submittingForm).toBe(true);
    userRegister.reject({});
    scope.$apply();
    expect(hasBeenCalled).toBe(true);
    expect(controller.submittingForm).toBe(false);
  });

  it('submitting registration has an error, should return messagea and reset captcha', function() {
    var controller = userRegistrationFormController();
    var hasBeenCalled = false;
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {
      hasBeenCalled = true;
    };
    userRegister.reject({ data: { modelState: { error: ['message'] } } });
    scope.$apply();

    expect(scope.registerErrors[0]).toBe('message');
    expect(hasBeenCalled).toBe(true);
  });

  it('submitting registration has an error, EIN is already registered message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['EIN is already registered'] } }
    });
    scope.$apply();

    expect(controller.einError).toBe(true);
  });

  it('submitting registration has an error, Unable to validate reCaptcha Response message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['Unable to validate reCaptcha Response'] } }
    });
    scope.$apply();

    expect(controller.reCaptchaError).toBe(true);
  });

  it('submitting registration has an error, Username already taken message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['is already taken'] } }
    });
    scope.$apply();

    expect(controller.emailAddressError).toBe(true);
  });

  it('submitting registration has an error, The Email field is required message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['The Email field is required.'] } }
    });
    scope.$apply();

    expect(controller.emailAddressRequired).toBe(true);
  });

  it('submitting registration has an error, The Password field is required. message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['The Password field is required.'] } }
    });
    scope.$apply();

    expect(controller.passwordRequired).toBe(true);
  });

  it('submitting registration has an error, The EIN field is required. message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['The EIN field is required.'] } }
    });
    scope.$apply();

    expect(controller.einRequired).toBe(true);
  });

  it('submitting registration has an error, The field EIN must match message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: { modelState: { error: ['The field EIN must match'] } }
    });
    scope.$apply();

    expect(controller.invalidEin).toBe(true);
  });

  it('submitting registration has an error, The password and confirmation password do not match. message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: {
        modelState: {
          error: ['The password and confirmation password do not match.']
        }
      }
    });
    scope.$apply();

    expect(controller.passwordsDontMatch).toBe(true);
  });

  it('submitting registration has an error, Password does not meet complexity requirements. message is displayed', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({
      data: {
        modelState: {
          error: ['Password does not meet complexity requirements.']
        }
      }
    });
    scope.$apply();

    expect(controller.passwordComplexity).toBe(true);
  });

  it('submitting registration has an error, log error details', function() {
    var controller = userRegistrationFormController();
    scope.onSubmitClick();
    scope.resetRegCaptcha = function() {};
    userRegister.reject({ data: { error: 'test' } });
    scope.$apply();
  });

  it('toggleEinHelp, on should turn off', function() {
    var controller = userRegistrationFormController();
    controller.showEinHelp = true;
    controller.toggleEinHelp();
    scope.$apply();

    expect(controller.showEinHelp).toBe(false);
  });

  it('toggleEinHelp, off should turn on', function() {
    var controller = userRegistrationFormController();
    controller.showEinHelp = false;
    controller.toggleEinHelp();
    scope.$apply();

    expect(controller.showEinHelp).toBe(true);
  });

  it('hideShowPassword, off should turn on', function() {
    var controller = userRegistrationFormController();
    controller.showEinHelp = false;
    controller.toggleEinHelp();
    scope.$apply();

    expect(controller.showEinHelp).toBe(true);
  });

  it('toggle hideShowPassword should show password if it is hidden', function() {
    var controller = userRegistrationFormController();
    scope.inputType = 'password';
    scope.hideShowPassword();
    scope.$apply();

    expect(scope.inputType).toBe('text');
  });

  it('toggle hideShowPassword should hide password if it is shown', function() {
    var controller = userRegistrationFormController();
    scope.inputType = 'text';
    scope.hideShowPassword();
    scope.$apply();

    expect(scope.inputType).toBe('password');
  });

  it('reset reCaptcha should reload and clear previous response', function() {
    spyOn(mockvcRecaptchaService, 'reload');
    var controller = userRegistrationFormController();
    scope.regResponse = 'existing-response';
    scope.resetRegCaptcha();
    scope.$apply();

    expect(scope.regResponse).toBe(null);
  });

  it('call email verification when code and userId are present', function() {
    spyOn(mockLocation, 'search').and.returnValue({
      code: 'code',
      userId: 'userId'
    });
    var controller = userRegistrationFormController();
    scope.$apply();

    expect(controller.isEmailVerificationRequest).toBe(true);
  });

  it('email verification success should show email verified message', function() {
    spyOn(mockLocation, 'search').and.returnValue({
      code: 'code',
      userId: 'userId'
    });
    var controller = userRegistrationFormController();
    emailVerification.resolve();
    scope.$apply();

    expect(controller.emailVerified).toBe(true);
  });

  it('email verification error should show error message', function() {
    spyOn(mockLocation, 'search').and.returnValue({
      code: 'code',
      userId: 'userId'
    });
    var controller = userRegistrationFormController();
    emailVerification.reject({});
    scope.$apply();

    expect(controller.emailVerificationError).toBe(true);
  });

  it('email verification error should show error message, log details', function() {
    spyOn(mockLocation, 'search').and.returnValue({
      code: 'code',
      userId: 'userId'
    });
    var controller = userRegistrationFormController();
    emailVerification.reject({ data: { error: 'test' } });
    scope.$apply();

    expect(controller.emailVerificationError).toBe(true);
  });

  it('valid password validation', function() {
    var controller = userRegistrationFormController();
    scope.formVals.pass = 'aB1#5678';
    scope.$apply();

    expect(controller.passwordLength).toBe(true);
    expect(controller.passwordUpper).toBe(true);
    expect(controller.passwordLower).toBe(true);
    expect(controller.passwordSpecial).toBe(true);
    expect(controller.passwordNumber).toBe(true);
  });
});
