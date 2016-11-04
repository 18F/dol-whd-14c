describe('accountGridController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        accountGridController = function() {
            return $controller('accountGridController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = accountGridController();
    });
});