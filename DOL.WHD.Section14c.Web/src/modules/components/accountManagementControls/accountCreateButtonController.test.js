describe('accountCreateButtonController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        accountCreateButtonController = function() {
            return $controller('accountCreateButtonController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = accountCreateButtonController();
    });
});

