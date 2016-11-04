describe('accountFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        accountFormController = function() {
            return $controller('accountFormController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = accountFormController();
    });
});