describe('authService', function() {
  beforeEach(module('14c'));

  var authService, $httpBackend, stateService;
  var env, $q, $rootScope;

  beforeEach(
    inject(function(
      _$httpBackend_,
      _authService_,
      __env_,
      _stateService_,
      _$q_,
      _$rootScope_
    ) {
      authService = _authService_;
      $httpBackend = _$httpBackend_;
      stateService = _stateService_;
      env = __env_;
      $q = _$q_;
      $rootScope = _$rootScope_;
    })
  );

  //userLogin
  it('userLogin error should reject deferred', function() {
    var isResolved;
    var result;
    authService.userLogin().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend.expectPOST(env.api_url + '/Token').respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('userLogin error should reject deferred if authenticateUser fails', function() {
    var isResolved;
    var result;
    var authenticateUser = $q.defer();
    spyOn(authService, 'authenticateUser').and.returnValue(
      authenticateUser.promise
    );
    authService.userLogin().then(undefined, function(error) {
      result = error;
      isResolved = false;
    });

    $httpBackend.expectPOST(env.api_url + '/Token').respond(200, 'value');
    $httpBackend.flush();
    authenticateUser.reject('error');
    $rootScope.$digest();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('error');
  });

  it('userLogin success should resolve deferred', function() {
    var isResolved;
    var authenticateUser = $q.defer();
    spyOn(authService, 'authenticateUser').and.returnValue(
      authenticateUser.promise
    );
    authService.userLogin().then(function() {
      isResolved = true;
    });

    $httpBackend.expectPOST(env.api_url + '/Token').respond(200, 'value');
    $httpBackend.flush();
    authenticateUser.resolve();
    $rootScope.$digest();
    expect(isResolved).toEqual(true);
  });

  it('authenticateUser should reject deferred', function() {
    var isResolved;
    var result;
    authService.authenticateUser().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('authenticateUser success should resolve deferred non admin', function() {
    var isResolved;
    var data = { organizations: [{ ein: '12-1234567' }] };
    var loadSavedApplication = $q.defer();
    spyOn(stateService, 'loadSavedApplication').and.returnValue(
      loadSavedApplication.promise
    );
    authService.authenticateUser().then(function() {
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(200, data);
    $httpBackend.flush();
    loadSavedApplication.resolve();
    $rootScope.$digest();
    expect(isResolved).toEqual(true);
    expect(stateService.loggedIn).toEqual(true);
    expect(stateService.user).toEqual(data);
    expect(stateService.ein).toEqual('12-1234567');
  });

  it('authenticateUser success should resolve deferred admin', function() {
    var isResolved;
    var data = {
      organizations: [{ ein: '12-1234567' }],
      applicationClaims: ['DOL.WHD.Section14c.Application.ViewAdminUI']
    };
    var loadApplicationList = $q.defer();
    spyOn(stateService, 'loadApplicationList').and.returnValue(
      loadApplicationList.promise
    );
    authService.authenticateUser().then(function() {
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(200, data);
    $httpBackend.flush();
    loadApplicationList.resolve();
    $rootScope.$digest();
    expect(isResolved).toEqual(true);
    expect(stateService.loggedIn).toEqual(true);
    expect(stateService.user).toEqual(data);
  });

  it('authenticateUser error should reject deferred non admin', function() {
    var result;
    var isResolved;
    var data = { organizations: [{ ein: '12-1234567' }] };
    var loadSavedApplication = $q.defer();
    spyOn(stateService, 'loadSavedApplication').and.returnValue(
      loadSavedApplication.promise
    );
    authService.authenticateUser().then(undefined, function(error) {
      result = error;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(200, data);
    $httpBackend.flush();
    loadSavedApplication.reject('error');
    $rootScope.$digest();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('error');
  });

  it('authenticateUser error should reject deferred admin', function() {
    var result;
    var isResolved;
    var data = {
      organizations: [{ ein: '12-1234567' }],
      applicationClaims: ['DOL.WHD.Section14c.Application.ViewAdminUI']
    };
    var loadApplicationList = $q.defer();
    spyOn(stateService, 'loadApplicationList').and.returnValue(
      loadApplicationList.promise
    );
    authService.authenticateUser().then(undefined, function(error) {
      result = error;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(200, data);
    $httpBackend.flush();
    loadApplicationList.reject('error');
    $rootScope.$digest();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('error');
  });
});
