describe('employerRegistrationController', function() {
  var scope, employerRegistrationController, organizations, formData, mockValidationService, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, validationService, _$q_, apiService) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;
      mockValidationService = validationService;

      employerRegistrationController = function() {
        return $controller('employerRegistrationController', {
          $scope: scope,
          apiService: mockApiService,
          validationService: mockValidationService
        });
      };
      controller = employerRegistrationController();

      setEmployer = $q.defer();
      spyOn(mockApiService, 'setEmployer').and.returnValue(
        setEmployer.promise
      );

      validateEIN = $q.defer();
      spyOn(mockValidationService, 'validateEIN').and.returnValue(
        validateEIN.promise
      );

      validateZipCode = $q.defer();
      spyOn(mockValidationService, 'validateZipCode').and.returnValue(
        validateZipCode.promise
      );

      validateCertificateNumber = $q.defer();
      spyOn(mockValidationService, 'validateCertificateNumber').and.returnValue(
        validateCertificateNumber.promise
      );

      scope.formData = {
        IsPointOfContact: true,
        employer: {
          ein: "12-12345",
          certificateNumber: "12-34567-H-890",
          physicalAddress: {
            streetAddress: "Test Street",
            state: "Test State",
            city: "Test City",
            county: "Test County",
            zipCode: "17050"
          },
          legalName: "Test Name",
          hasTradeName: "Test Name"
        }
      }
      scope.registrationSuccess = false;
    })
  );

  it('invoke controller', function() {
    expect(controller).toBeDefined();
    expect(scope.formIsValid).toBe(true);
    expect(scope.registrationSuccess).toBe(false);
    expect(scope.previouslyRegistered.status).toBe(false);
    expect(scope.previouslyRegistered.name).toEqual("");
  });

  it('initialization of properties', function() {
    controller = employerRegistrationController();
    expect(scope.formData.employer).toBeDefined();
    expect(scope.formData.employer.physicalAddress).toBeDefined();
  });

  it('submission error causes registration error', function() {
    scope.onSubmitClick();
    setEmployer.reject();
    scope.$apply();
    expect(scope.registrationSuccess).toBe(false);
    expect(scope.previouslyRegistered.status).toBe(false);
    expect(scope.previouslyRegistered.name).toEqual("");
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
    expect(scope.formIsValid).toBe(true);
    expect(scope.registrationSuccess).toBe(true);
    expect(mockValidationService.validateEIN).toHaveBeenCalled();
    expect(mockValidationService.validateZipCode).toHaveBeenCalled();
    expect(mockValidationService.validateCertificateNumber).toHaveBeenCalled();
  });

  it('submission error causes registration error', function() {
    scope.onSubmitClick();
    setEmployer.reject();
    scope.$apply();
    expect(scope.registrationSuccess).toBe(false);
  });

  it('does not call setEmployer or validation service if required values are undefined', function() {
    scope.formData.employer = {
      physicalAddress: {}
    };
    scope.formData.employer.hasTradeName = true;
    scope.onSubmitClick();
    scope.$apply();
    expect(scope.formIsValid).toBe(false);
    expect(mockApiService.setEmployer).not.toHaveBeenCalled();
    expect(mockValidationService.validateEIN).not.toHaveBeenCalled();
    expect(mockValidationService.validateZipCode).not.toHaveBeenCalled();
    expect(mockValidationService.validateCertificateNumber).not.toHaveBeenCalled();
  });

  it('does not call setEmployer if validation errors exist', function() {
    scope.formData.employer = {
      physicalAddress: {}
    };
    scope.onSubmitClick();
    scope.$apply();
    expect(mockApiService.setEmployer).not.toHaveBeenCalled();
  });

  it('validates employer address', function() {
    scope.validateAddress();
    scope.validateForm();
    scope.$apply();
    expect(scope.formIsValid).toBe(true);
    scope.formData.employer.physicalAddress = {};
    scope.validateAddress();
    scope.validateForm();
    scope.$apply();
    expect(scope.formIsValid).toBe(false);
    spyOn(scope, 'validateAddress');
    scope.getValidationErrors();
    expect(scope.validateAddress).toHaveBeenCalled();
  });

  it('toggles all help text', function() {
    expect(scope.showAllHelp).toBe(false);
    scope.toggleAllHelpText();
    expect(scope.showAllHelp).toBe(true);
  });


});
