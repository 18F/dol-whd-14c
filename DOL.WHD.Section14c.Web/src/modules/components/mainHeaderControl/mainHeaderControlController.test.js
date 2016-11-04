describe('mainHeaderControlController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, navService) {
        scope = $rootScope.$new();
        mockNavService = navService;

        spyOn(mockNavService,'hasNext');
        spyOn(mockNavService,'hasBack');
        spyOn(mockNavService,'getNextSection');

        mainHeaderControlController = function() {
            return $controller('mainHeaderControlController', {
                '$scope': scope, 
                'navService': mockNavService
            });
        };
    }));

    it('invoke controller', function() {
        var controller = mainHeaderControlController();
    });
});