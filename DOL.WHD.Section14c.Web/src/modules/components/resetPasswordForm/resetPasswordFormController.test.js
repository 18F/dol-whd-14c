describe('resetPasswordFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        resetPasswordFormController = function() {
            return $controller('resetPasswordFormController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = resetPasswordFormController();
    });
});