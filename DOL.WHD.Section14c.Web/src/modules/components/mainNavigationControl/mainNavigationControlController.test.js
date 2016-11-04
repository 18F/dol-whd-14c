describe('mainNavigationControlController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, $route, navService) {
        scope = $rootScope.$new();
        route = $route;
        mockNavService = navService;

        spyOn(mockNavService,'hasNext');
        spyOn(mockNavService,'hasBack');

        route.current = {
            params: { section_id : 1 }
        }

        spyOn(mockNavService,'getNextSection');

        mainNavigationControlController = function() {
            return $controller('mainNavigationControlController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = mainNavigationControlController();
    });
});