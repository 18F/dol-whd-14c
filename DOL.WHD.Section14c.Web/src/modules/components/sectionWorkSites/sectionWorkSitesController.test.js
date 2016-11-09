describe('sectionWorkSitesController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();
        scope.responses = {
            "PrimaryDisability": [
                {
                    "id": 31,
                    "display": "Intellectual/Developmental Disability (IDD)",
                    "otherValueKey": null
                },
                {
                    "id": 38,
                    "display": "Other, please specify:",
                    "otherValueKey": "primaryDisabilityOther"
                }
            ]
        }

        sectionWorkSitesController = function() {
            return $controller('sectionWorkSitesController', {
                '$scope': scope,
                '$route': route
            });
        };
    }));

    it('invoke controller', function() {
        var controller = sectionWorkSitesController();
    });

    it('sectionWorkSitesController has clearActiveWorker', function() {
        var controller = sectionWorkSitesController();
        controller.clearActiveWorker();
    });

    it('sectionWorkSitesController has addEmployee', function() {
        var controller = sectionWorkSitesController();
        controller.addEmployee();
    });
    it('sectionWorkSitesController has doneAddingEmployees', function() {
        var controller = sectionWorkSitesController();
        controller.doneAddingEmployees();
    });
    it('sectionWorkSitesController has editEmployee', function() {
        var controller = sectionWorkSitesController();
        controller.activeWorksite = { employees: [] }
        controller.editEmployee();
    });
    it('sectionWorkSitesController has addWorkSite', function() {
        var controller = sectionWorkSitesController();
        controller.addWorkSite();
    });
    it('sectionWorkSitesController has saveWorkSite', function() {
        var controller = sectionWorkSitesController();
        controller.saveWorkSite();
    });
    it('sectionWorkSitesController has editWorkSite', function() {
        var controller = sectionWorkSitesController();
        controller.editWorkSite();
    });
    it('sectionWorkSitesController has deleteWorkSite', function() {
        var controller = sectionWorkSitesController();
        controller.deleteWorkSite();
    });
    it('sectionWorkSitesController has setActiveTab', function() {
        var controller = sectionWorkSitesController();
        controller.setActiveTab();
        controller.setActiveTab(2);
    });
    it('sectionWorkSitesController has siteRowClicked', function() {
        var controller = sectionWorkSitesController();
        controller.siteRowClicked({ target: '' });
    });
    it('sectionWorkSitesController has getDisabilityDisplay', function() {
        var controller = sectionWorkSitesController();
        controller.getDisabilityDisplay();
    });
    it('sectionWorkSitesController has validateActiveWorksiteProperty', function() {
        var controller = sectionWorkSitesController();
        controller.validateActiveWorksiteProperty();
    });
    it('sectionWorkSitesController has validateActiveWorkerProperty', function() {
        var controller = sectionWorkSitesController();
        controller.validateActiveWorkerProperty();
    });
    it('sectionWorkSitesController has validateActiveWorksiteWorker', function() {
        var controller = sectionWorkSitesController();
        controller.validateActiveWorksiteWorker();
    });

    // test add/edit/remove workers and worksites
    it('should add/edit/delete a worker and a worksite', function() {
        var controller = sectionWorkSitesController();

        controller.activeWorksite = {};
        controller.activeWorker = {
            "name": "First Last",
            "primaryDisability": 31
        };

        controller.getDisabilityDisplay(controller.activeWorker);

        controller.addAnotherEmployee();
        controller.doneAddingEmployees();
        expect(controller.activeWorksite.employees.length).toBe(1);

        controller.editEmployee(0);
        controller.activeWorker.primaryDisability = 38;
        controller.activeWorker.primaryDisabilityId = 38;
        controller.activeWorker.primaryDisabilityOther = "other";

        controller.getDisabilityDisplay(controller.activeWorker);

        controller.addEmployee();
        expect(controller.activeWorksite.employees.length).toBe(1);

        controller.deleteEmployee(0);
        expect(controller.activeWorksite.employees.length).toBe(0);

        controller.saveWorkSite();
        expect(scope.formData.workSites.length).toBe(1);

        controller.editWorkSite(0);
        controller.addWorkSite();
        expect(scope.formData.workSites.length).toBe(1);

        controller.deleteWorkSite(0);
        expect(scope.formData.workSites.length).toBe(0);
    });

    it('should get employee disability', function() {
        var controller = sectionWorkSitesController();
        var employee = { "primaryDisability": 32 };
    });

    it('should validate worksite and workery properties', function() {
        var controller = sectionWorkSitesController();

        controller.activeWorksiteIndex = 0;
        controller.validateActiveWorksiteProperty("name");

        controller.activeWorkerIndex = 0;
        controller.validateActiveWorkerProperty("name");

        controller.validateActiveWorksiteWorker(0);
    })
});
