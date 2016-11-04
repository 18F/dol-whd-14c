describe('wageDataPayTypeFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        wageDataPayTypeFormController = function() {
            return $controller('wageDataPayTypeFormController', {
                '$scope': scope, 
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = wageDataPayTypeFormController();
    });
});