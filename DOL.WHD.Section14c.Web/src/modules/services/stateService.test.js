describe('stateService', function() {
  beforeEach(module('14c'));

  var stateService, apiService, $q, getApplication
  var getSubmittedApplication, getSubmittedApplications, $rootScope;

  beforeEach(
    inject(function(_$rootScope_, _$q_, _stateService_, _apiService_) {
      stateService = _stateService_;
      apiService = _apiService_;
      $q = _$q_;
      getApplication = $q.defer();
      getSubmittedApplication = $q.defer();
      getSubmittedApplications = $q.defer();
      $rootScope = _$rootScope_;
      spyOn(apiService, 'getApplication').and.returnValue(
        getApplication.promise
      );
      spyOn(apiService, 'getSubmittedApplication').and.returnValue(
        getSubmittedApplication.promise
      );
      spyOn(apiService, 'getSubmittedApplications').and.returnValue(
        getSubmittedApplications.promise
      );
    })
  );

  it('should set form data', function() {
    stateService.setFormData({ testProperty: 1234 });
    expect(stateService.formData.testProperty).toEqual(1234);
  });

  it('should set form value', function() {
    stateService.setFormValue('testProperty', 'testValue');
    expect(stateService.formData.testProperty).toEqual('testValue');
  });

  it('should set the user property', function() {
    stateService.user = 'value';
    expect(stateService.user).toEqual('value');
  });

  it('should set the ein property', function() {
    stateService.ein = 'value';
    expect(stateService.ein).toEqual('value');
  });

  it('should set the formData property', function() {
    stateService.formData = 'value';
    expect(stateService.formData).toEqual('value');
  });

  it('should clear the application state on logout', function() {
    stateService.logOut();
    expect(stateService.activeEIN).toEqual(undefined);
  });

  it('should return if the user has a claim', function() {
    var hasClaim = stateService.hasClaim();
    expect(hasClaim).toEqual(false);
  });

  it('should return if the user has a claim', function() {
    var claimName = 'test';
    stateService.user = {
      applicationClaims: ['DOL.WHD.Section14c.' + claimName]
    };
    var hasClaim = stateService.hasClaim(claimName);
    expect(hasClaim).toEqual(true);
  });

  it('should load user info and application data', function() {
    stateService.loadSavedApplication();
    getApplication.resolve({ data: '{ "applicationTypeId": 1 }' });
    $rootScope.$digest();

    expect(stateService.formData.applicationTypeId).toEqual(1);
  });

  it('should load user info and fail on application data', function() {
    var result;
    var isResolved;
    stateService.loadSavedApplication().then(undefined, function(error) {
      result = error;
      isResolved = false;
    });
    getApplication.reject('error');
    $rootScope.$digest();

    expect(result).toEqual('error');
    expect(isResolved).toEqual(false);
  });

  it('should load submitted application data', function() {
    var result;
    var isResolved;
    var data = { data: '{ "applicationTypeId": 1 }' };
    stateService.loadApplicationData().then(function(data) {
      isResolved = true;
      result = data;
    });
    getSubmittedApplication.resolve(data);
    $rootScope.$digest();

    expect(isResolved).toEqual(true);
    expect(result).toEqual(data.data);
  });

  it('should fail loading submitted application data', function() {
    var result;
    var isResolved;
    stateService.loadApplicationData().then(undefined, function(error) {
      isResolved = false;
      result = error;
    });
    getSubmittedApplication.reject('error');
    $rootScope.$digest();

    expect(isResolved).toEqual(false);
    expect(result).toEqual('error');
  });

  it('should load application list', function() {
    var result;
    var isResolved;
    var data = { data: '[{ "applicationTypeId": 1 }]' };
    stateService.loadApplicationList().then(function(data) {
      isResolved = true;
      result = data;
    });
    getSubmittedApplications.resolve(data);
    $rootScope.$digest();

    expect(isResolved).toEqual(true);
    expect(result).toEqual(data.data);
  });

  it('should fail loading application list', function() {
    var result;
    var isResolved;
    stateService.loadApplicationList().then(undefined, function(error) {
      isResolved = false;
      result = error;
    });
    getSubmittedApplications.reject('error');
    $rootScope.$digest();

    expect(isResolved).toEqual(false);
    expect(result).toEqual('error');
  });
});
