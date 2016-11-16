describe('sectionAppInfoController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, responsesService) {
        scope = $rootScope.$new();
        mockResponsesService = responsesService;

        sectionAppInfoController = function() {
            return $controller('sectionAppInfoController', {
                '$scope': scope, 
                'navService': mockNavService,
                '$route': route,
                'responsesService': mockResponsesService
            });
        };
    }));

    it('toggleEstablishmentType toggle on', function() {
        var controller = sectionAppInfoController();
        scope.formData.establishmentTypeId = [];
        controller.toggleEstablishmentType(1);

        expect(scope.formData.establishmentTypeId.indexOf(1)).toBe(0)
    });

    it('toggleEstablishmentType toggle off', function() {
        var controller = sectionAppInfoController();
        scope.formData.establishmentTypeId = [1];
        controller.toggleEstablishmentType(1);

        expect(scope.formData.establishmentTypeId.indexOf(1)).toBe(-1)
    });    

});