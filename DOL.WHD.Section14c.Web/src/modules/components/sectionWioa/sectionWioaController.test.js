describe('sectionWioaController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        sectionWioaController = function() {
            return $controller('sectionWioaController', {
                '$scope': scope, 
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionWioaController();
    });
});