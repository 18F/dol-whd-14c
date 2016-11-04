describe('sectionEmployerController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        sectionEmployerController = function() {
            return $controller('sectionEmployerController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionEmployerController();
    });
});