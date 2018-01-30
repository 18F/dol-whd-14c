describe('employerRegistrationController', function() {
  var scope, employerRegistrationController, organizations, formData, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService, stateService) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;
      mockStateService = stateService;

      scope.organizations = [
        {
          employer: {
            id: '1234',
            legalName: 'Test Employer1',
            ein: '2',
            physicalAddress: {
              streetAddress: 'Test',
              city:'Mechanicsburg',
              state: 'PA',
              zipCode: '17050',
            }
          },
          createdAt:'',
          lastModifiedAt:'',
          applicationStatus: {
            name: "New"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        },
        {
          employer: {
            id: '1234',
            legalName: 'Test Employer',
            ein: '2',
            physicalAddress: {
              streetAddress: 'Test',
              city:'Mechanicsburg',
              state: 'PA',
              zipCode: '17050',
            }
          },
          createdAt:'',
          lastModifiedAt:'',
          applicationStatus: {
            name: "Submitted"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        }
      ];


      employerRegistrationController = function() {
        return $controller('employerRegistrationController', {
          $scope: scope,
          apiService: mockApiService,
          stateService: mockStateService
        });
      };
      controller = employerRegistrationController();

      setEmployer = $q.defer();
      spyOn(mockApiService, 'setEmployer').and.returnValue(
        setEmployer.promise
      );

      userInfo = $q.defer();
      spyOn(mockApiService, 'userInfo').and.returnValue(
        userInfo.promise
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
    userInfo.resolve({data:{organizations: scope.organizations}});
    scope.$apply();
    expect(mockApiService.userInfo).toHaveBeenCalled();
    expect(scope.registrationSuccess).toBe(true);
    expect(scope.application.ein).toEqual('12-123345');
    expect(mockStateService.ein).toEqual('12-123345');
    expect(mockStateService.applicationId).toEqual('1231541515');
    expect(mockStateService.employerName).toEqual('Test Employer1');
  });

  it('submission error causes registration error', function() {
    scope.onSubmitClick();
    setEmployer.resolve();
    scope.$apply();
    expect(scope.registrationSuccess).toBe(true);
  });
});
