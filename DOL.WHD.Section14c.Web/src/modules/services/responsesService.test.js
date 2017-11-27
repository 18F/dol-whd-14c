describe('responsesService', function() {
  var deferred, mockResponsesService;
  var $httpBackend, env;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $injector,
      _$q_,
      _$httpBackend_,
      responsesService,
      _env
    ) {
      deferred = _$q_.defer();
      mockResponsesService = responsesService;
      $httpBackend = _$httpBackend_;
      env = _env;
    })
  );

  it('getQuestionResponses should reject promise', function() {
    var questionKeys = ['TestResponseService'];
    deferred.resolve('Returned OK!');
    deferred = mockResponsesService.getQuestionResponses(questionKeys);

    expect(deferred.resolve).toEqual(undefined);
  });

  it('getQuestionResponses should reject promise', function() {
    var isResolved;
    var result;
    mockResponsesService
      .getQuestionResponses()
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend.expectGET(env.api_url + '/api/Response').respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('getQuestionResponses should resolve promise', function() {
    var isResolved;
    mockResponsesService.getQuestionResponses().then(function() {
      isResolved = true;
    });

    $httpBackend.expectGET(env.api_url + '/api/Response').respond(200, [
      {
        id: 1,
        questionKey: 'ApplicationType',
        display: 'Initial Application',
        subDisplay: null,
        otherValueKey: null,
        isActive: true
      }
    ]);
    $httpBackend.flush();

    // load cached values
    mockResponsesService.getQuestionResponses().then(function() {
      isResolved = true;
    });
    expect(isResolved).toEqual(true);
  });

  it('getQuestionResponses should resolve promise, question array', function() {
    var isResolved;
    mockResponsesService.getQuestionResponses([]).then(function() {
      isResolved = true;
    });

    $httpBackend.expectGET(env.api_url + '/api/Response').respond(200, [
      {
        id: 1,
        questionKey: 'ApplicationType',
        display: 'Initial Application',
        subDisplay: null,
        otherValueKey: null,
        isActive: true
      }
    ]);
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
  });
});
