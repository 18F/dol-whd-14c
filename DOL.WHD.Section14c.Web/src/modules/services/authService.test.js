describe('authService', function() {
    beforeEach(module('14c'));
    
    beforeEach(inject(function(_$httpBackend_, _authService_, __env_, _stateService_) {
        authService = _authService_;
        $httpBackend = _$httpBackend_;
        stateService = _stateService_;
        env = __env_;
    }));

    //userLogin
    it('userLogin error should reject deferred', function() {   
        var isResolved;
        var result;
        authService.userLogin().then(undefined, function (error) {
            result = error.data;
            isResolved = false;
        });

        $httpBackend.expectPOST(env.api_url + '/Token').respond(400, 'value');
        $httpBackend.flush();
        expect(isResolved).toEqual(false);
        expect(result).toEqual('value');
    });
});