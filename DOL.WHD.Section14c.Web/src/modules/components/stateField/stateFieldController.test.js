describe('stateFieldController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, _$q_, apiService) {
        scope = $rootScope.$new();
        $q = _$q_;
        mockApiService = apiService;

        stateFieldController = function() {
            return $controller('stateFieldController', {
                '$scope': scope
            });
        };


    }));

    it('state value set', function() {
        scope.selectedState = 'DE';
        var controller = stateFieldController();
        
        expect(controller.stateVal).toBe('DE');
    });    
     
    it('seleted state gets updated', function() {
        var controller = stateFieldController();
        scope.vm = controller;
        controller.stateVal = 'DE';
        scope.$apply();

        expect(scope.selectedState).toBe('DE');
    });    
});