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
        deferred = responses.getQuestionResponses(questionKeys);

        expect(deferred.resolve).toEqual(undefined);
    });
    
    it('getQuestionResponses should reject promise', function () {

        var questionKeys = [ 'TestResponseService' ];
        deferred.reject('There has been an Error!');

        deferred = responses.getQuestionResponses(questionKeys);

        expect(deferred.reject).toBe(undefined);
    });


});