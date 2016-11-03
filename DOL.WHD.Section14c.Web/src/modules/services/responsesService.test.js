describe('responsesService', function() {
    var deferred;
    var $q;
    var $rootScope;

    beforeEach(module('14c'));

    var responses;

    beforeEach(inject(function($injector, _$rootScope_, _$q_) {
        $q = _$q_;
        $rootScope = _$rootScope_;
        deferred = _$q_.defer();
        responses = $injector.get('responsesService');
    }));

    it('getQuestionResponses should resolve promise', function () { 
   
        var questionKeys = [ 'TestResponseService' ];
        var something;
        deferred.resolve('Returned OK!');
        responses.getQuestionResponses(questionKeys);

        expect(responses.resolve).toBe('Returned OK!');
    });
    
    it('getQuestionResponses should reject promise', function () {

        var questionKeys = [ 'TestResponseService' ];
        deferred.reject('There has been an Error!');

        responses.getQuestionResponses(questionKeys);

        expect(responses.reject).toBe('There has been an Error!');
    });


});