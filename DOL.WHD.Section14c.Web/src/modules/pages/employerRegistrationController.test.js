describe('employerRegistrationController', function() {
  var scope, employerRegistrationController, organizations, formData, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;

      employerRegistrationController = function() {
        return $controller('employerRegistrationController', {
          $scope: scope,
          apiService: mockApiService
        });
      };
      controller = employerRegistrationController();

      setEmployer = $q.defer();
      spyOn(mockApiService, 'setEmployer').and.returnValue(
        setEmployer.promise
      );

      deleteAttachment = $q.defer();
      spyOn(mockApiService, 'deleteAttachment').and.returnValue(
        deleteAttachment.promise
      );

      scope.formData = {
        IsPointOfContact: true,
        employer: {
          ein: "12-12345"
        }
      }
      scope.registrationSuccess = false;
    })
  );

  it('invoke controller', function() {
    expect(controller).toBeDefined();
  });

  it('submission error causes registration error', function() {
    scope.onSubmitClick();
    setEmployer.reject();
    scope.$apply();
    expect(scope.registrationSuccess).toBe(false);
    expect(scope.previouslyRegistered).toBe(undefined);
  });

  it('submission error causes registration error for previously registered employer', function() {
    scope.onSubmitClick();
    setEmployer.reject({status: 302, data:'Test POC'});
    scope.$apply();
    expect(scope.registrationSuccess).toBe(false);
    expect(scope.previouslyRegistered.status).toBe(true);
    expect(scope.previouslyRegistered.name).toEqual('Test POC');
  });

  it('submission success causes registration sucess', function() {
    scope.onSubmitClick();
    setEmployer.resolve();
    scope.$apply();
    expect(scope.registrationSuccess).toBe(true);
  });

  it('submission error causes registration error', function() {
    scope.onSubmitClick();
    setEmployer.resolve();
    scope.$apply();
    expect(scope.registrationSuccess).toBe(true);
  });
});
