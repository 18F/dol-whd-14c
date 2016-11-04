describe('dateFieldController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        dateFieldController = function() {
            return $controller('dateFieldController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = dateFieldController();
    });
});