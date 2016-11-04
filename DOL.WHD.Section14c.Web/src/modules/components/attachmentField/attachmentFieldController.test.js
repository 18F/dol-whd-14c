describe('attachmentFieldController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        attachmentFieldController = function() {
            return $controller('attachmentFieldController', {
                '$scope': scope
            });
        };
    }));

    it('invoke controller', function() {
        var controller = attachmentFieldController();
    });
});