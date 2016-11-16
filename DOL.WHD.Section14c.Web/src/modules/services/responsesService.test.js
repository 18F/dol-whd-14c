describe('responsesService', function() {
    var deferred;
    var $q;
    var $rootScope;

    beforeEach(module('14c'));

    var responses;

    beforeEach(inject(function($injector, _$rootScope_, _$q_, _$httpBackend_, responsesService, _env) {
        $q = _$q_;
        $rootScope = _$rootScope_;
        deferred = _$q_.defer();
        mockResponsesService = responsesService;
        $httpBackend = _$httpBackend_;
        env = _env;

        changePassword = $q.defer();
        spyOn(mockApiService, 'changePassword').and.returnValue(changePassword.promise);
    }));

    it('getQuestionResponses should reject promise', function () { 
   
        var questionKeys = [ 'TestResponseService' ];
        var something;
        deferred.resolve('Returned OK!');
        deferred = mockResponsesService.getQuestionResponses(questionKeys);

        expect(deferred.resolve).toEqual(undefined);
    });
    
    it('getQuestionResponses should reject promise', function () {
        var isResolved;
        var result;
        mockResponsesService.getQuestionResponses().then(undefined, function (error) {
            result = error.data;
            isResolved = false;
        });

        $httpBackend.expectGET(env.api_url + '/api/Response').respond(400, 'value');
        $httpBackend.flush();
        expect(isResolved).toEqual(false);
        expect(result).toEqual('value');
    });

    it('getQuestionResponses should resolve promise', function () {
        var isResolved;
        var result;
        mockResponsesService.getQuestionResponses().then(function (data) {
            result = data;
            isResolved = true;
        });

        $httpBackend.expectGET(env.api_url + '/api/Response').respond(200, [{"id":1,"questionKey":"ApplicationType","display":"Initial Application","subDisplay":null,"otherValueKey":null,"isActive":true}]);
        $httpBackend.flush();

        // load cached values
        mockResponsesService.getQuestionResponses().then(function (data) {
            result = data;
            isResolved = true;
        });
        expect(isResolved).toEqual(true);
    });


    it('getQuestionResponses should resolve promise, question array', function () {
        var isResolved;
        var result;
        mockResponsesService.getQuestionResponses([]).then(function (data) {
            result = data;
            isResolved = true;
        });

        $httpBackend.expectGET(env.api_url + '/api/Response').respond(200, [{"id":1,"questionKey":"ApplicationType","display":"Initial Application","subDisplay":null,"otherValueKey":null,"isActive":true}]);
        $httpBackend.flush();
        expect(isResolved).toEqual(true);
    });    


    
});

