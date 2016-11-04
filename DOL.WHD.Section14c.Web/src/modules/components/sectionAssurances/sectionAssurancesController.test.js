describe('sectionAssurancesController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        sectionAssurancesController = function() {
            return $controller('sectionAssurancesController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionAssurancesController();
    });
});