describe('sectionWioaController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();

        sectionWioaController = function() {
            return $controller('sectionWioaController', {
                '$scope': scope,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionWioaController();
    });

    it('should toggle learn more', function() {
        var controller = sectionWioaController();

        controller.toggleLearnMore();
    });

    it('should add/edit/delete worker', function() {
        var controller = sectionWioaController();

        controller.activeWorker = { "fullName": "Worker" };
        controller.addWorker();
        expect(scope.formData.WIOA.WIOAWorkers.length).toBe(1);

        controller.editWorker(0);
        controller.addWorker();
        expect(scope.formData.WIOA.WIOAWorkers.length).toBe(1);

        controller.deleteWorker(0);
        expect(scope.formData.WIOA.WIOAWorkers.length).toBe(0);

        controller.cancelAddWorker();
    });
});
