describe('sectionReviewController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        sectionReviewController = function() {
            return $controller('sectionReviewController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionReviewController();
    });
});