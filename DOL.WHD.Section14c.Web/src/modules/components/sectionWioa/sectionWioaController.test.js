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


    it('should toggle learn more on', function() {
        var controller = sectionWioaController();
        scope.showWIOAReqs = false;
        controller.toggleLearnMore();
        
        expect(scope.showWIOAReqs).toBe(true);
    });

    it('should toggle learn more off', function() {
        var controller = sectionWioaController();
        scope.showWIOAReqs = true;
        controller.toggleLearnMore();
        
        expect(scope.showWIOAReqs).toBe(false);
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
