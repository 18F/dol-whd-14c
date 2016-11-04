describe('accountCreateButtonController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, $location) {
        scope = $rootScope.$new();
        mockLocation = $location;

        accountCreateButtonController = function() {
            return $controller('accountCreateButtonController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = accountCreateButtonController();
    });

    it('create account click takes you to /account/create', function() {
        var controller = accountCreateButtonController();
        controller.createAccountClick();
        expect(mockLocation.path()).toBe("/account/create");

    });
});

