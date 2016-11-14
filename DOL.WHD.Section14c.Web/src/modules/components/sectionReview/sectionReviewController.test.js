describe('sectionReviewController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, apiService) {
        scope = $rootScope.$new();
        mockApiService = apiService;

        sectionReviewController = function() {
            return $controller('sectionReviewController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route
            });
        };
    }));

    it('submitApplication invalid form', function() {
        var controller = sectionReviewController();
        spyOn(mockApiService, 'submitApplication')
        scope.isValid = false;
        controller.onSubmit();

        expect(mockApiService.submitApplication).not.toHaveBeenCalled()
    });

    it('submitApplication valid form', function() {
        var controller = sectionReviewController();
        spyOn(mockApiService, 'submitApplication')
        scope.isValid = true;
        controller.onSubmit();

        expect(mockApiService.submitApplication).toHaveBeenCalled()
    });    
});