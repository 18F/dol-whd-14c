describe('sectionWageDataController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        sectionWageDataController = function() {
            return $controller('sectionWageDataController', {
                '$scope': scope, 
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionWageDataController();
    });
});