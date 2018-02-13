describe('accountGridController', function() {
  var scope, $q, mockApiService, mockAdminApiService, mockLocation, mockStateService;
  var accountGridController, getAccounts;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService, stateService, adminApiService, $location) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;
      mockStateService = stateService;
      mockAdminApiService = adminApiService;
      mockLocation = $location;

      accountGridController = function() {
        return $controller('accountGridController', {
          $scope: scope,
          apiService: mockApiService,
          stateService: mockStateService,
          adminApiService: mockAdminApiService
        });
      };

      getAccounts = $q.defer();
      spyOn(mockApiService, 'getAccounts').and.returnValue(getAccounts.promise);

      resetPassword = $q.defer();
      spyOn(mockAdminApiService, 'resetPassword').and.returnValue(resetPassword.promise);

      resendCode = $q.defer();
      spyOn(mockAdminApiService, 'resendCode').and.returnValue(resendCode.promise);

      resendConfirmationEmail = $q.defer();
      spyOn(mockAdminApiService, 'resendConfirmationEmail').and.returnValue(resendConfirmationEmail.promise);

      mockStateService.access_token = '1234';

    })
  );

  it('invoke controller', function() {
    var controller = accountGridController();
    expect(controller).toBeDefined();
  });

  it('accounts load', function() {
    accountGridController();
    getAccounts.resolve({ data: [{ userId: 1 }] });
    scope.$apply();

    expect(scope.accounts.length).toBe(1);
    expect(scope.accounts[0].userId).toBe(1);
  });

  it('accounts loading failure displays an error, error description', function() {
    var controller = accountGridController();
    getAccounts.reject({ data: { error: {} } });
    scope.$apply();
    expect(controller.loadingError).toBe(true);
  });

  it('edit account click navigates to /account/{userId}', function() {
    var controller = accountGridController();
    spyOn(mockLocation, 'path');
    controller.editAccountClick(1);
    expect(mockLocation.path).toHaveBeenCalledWith('/account/1');
  });

  it('resendCode success should resolve deferred', function() {
    var controller = accountGridController();
    scope.resendCode();
    resendCode.resolve({status: 200, data: {code:'test'}});
    scope.$apply();
    expect(mockAdminApiService.resendCode).toHaveBeenCalled();
    expect(controller.update.status).toEqual('Success');
  });

  it('resendCode failure should reject deferred', function() {
    var controller = accountGridController();
    scope.resendCode();
    resendCode.reject();
    scope.$apply();
    expect(mockAdminApiService.resendCode).toHaveBeenCalled();
    expect(controller.update.status).toEqual('Failure');
  });

  it('resetPassword success should resolve deferred', function() {
    var controller = accountGridController();
    scope.userEmail = "test";
    scope.password = "test";
    scope.confirmPassword = "test";
    scope.resetPassword();
    resetPassword.resolve({status: 200});
    scope.$apply();
    expect(mockAdminApiService.resetPassword).toHaveBeenCalled();
    expect(controller.update.status).toEqual('Success');
  });

  it('resetPassword failure should reject deferred', function() {
    var controller = accountGridController();
    scope.resetPassword();
    resetPassword.reject();
    scope.$apply();
    expect(mockAdminApiService.resetPassword).toHaveBeenCalled();
    expect(controller.update.status).toEqual('Failure');
  });

  it('resendConfirmationEmail success should resolve deferred', function() {
    var controller = accountGridController();
    scope.resendConfirmationEmail();
    resendConfirmationEmail.resolve({status: 200, data: {code:'test'}});
    scope.$apply();
    expect(mockAdminApiService.resendConfirmationEmail).toHaveBeenCalled();
    expect(controller.update.status).toEqual('Success');
  });

  it('resendConfirmationEmail failure should reject deferred', function() {
    var controller = accountGridController();
    scope.resendConfirmationEmail();
    resendConfirmationEmail.reject();
    scope.$apply();
    expect(mockAdminApiService.resendConfirmationEmail).toHaveBeenCalled();
    expect(controller.update.status).toEqual('Failure');
  });

  it('submit should use right api call', function() {
    var controller = accountGridController();
    scope.resendCodeModalIsVisible = true;
    scope.submit();
    expect(mockAdminApiService.resendCode).toHaveBeenCalled();
    scope.resendCodeModalIsVisible = false;
    scope.resetPasswordModalIsVisible = true;
    scope.submit();
    expect(mockAdminApiService.resetPassword).toHaveBeenCalled();
    scope.resetPasswordModalIsVisible = false;
    scope.resendEmailModalIsVisible = true;
    scope.submit();
    expect(mockAdminApiService.resendConfirmationEmail).toHaveBeenCalled();
  });
});
