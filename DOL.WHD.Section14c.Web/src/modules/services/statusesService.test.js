describe('apiService', function() {
  beforeEach(module('14c'));

  var statusesService, $httpBackend, env, $rootScope;

  beforeEach(
    inject(function(
      $injector,
      _$httpBackend_,
      _statusesService_,
      _env,
      _$rootScope_
    ) {
      statusesService = _statusesService_;
      $httpBackend = _$httpBackend_;
      env = _env;
      $rootScope = _$rootScope_;
    })
  );

  //getStatuses
  it('getStatuses from server error should reject deferred', function() {
    var isResolved;
    var result;
    statusesService.getStatuses().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend.expectGET(env.api_url + '/api/status').respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('getStatuses from server success should resolve deferred', function() {
    var isResolved;
    var result;
    statusesService.getStatuses().then(function(data) {
      result = data;
      isResolved = true;
    });

    $httpBackend.expectGET(env.api_url + '/api/status').respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  it('getStatuses from cache success should resolve deferred', function() {
    var isResolved;
    var result;
    statusesService.getStatuses();

    $httpBackend.expectGET(env.api_url + '/api/status').respond(200, 'value');
    $httpBackend.flush();

    // make a second request to get data from the cache
    statusesService.getStatuses().then(function(data) {
      result = data;
      isResolved = true;
    });
    $rootScope.$apply();

    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });
});
