describe('changePasswordFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        changePasswordFormController = function() {
            return $controller('changePasswordFormController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = changePasswordFormController();
    });
});