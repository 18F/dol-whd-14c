describe('sectionWorkSitesController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, validationService, $document) {
      scope = $rootScope.$new();
      mockValidationService = validationService;
      scope.responses = {
        PrimaryDisability: [
          {
            id: 31,
            display: 'Intellectual/Developmental Disability (IDD)',
            otherValueKey: null
          },
          {
            id: 38,
            display: 'Other, please specify:',
            otherValueKey: 'primaryDisabilityOther'
          }
        ]
      };

      scope.worker = {
        avgHourlyEarnings:0.1,
        avgWeeklyHours:0.1,
        commensurateWageRate:0.1,
        hasProductivityMeasure:true,
        name:"zxcv",
        numJobs:4,
        prevailingWage:0.1,
        primaryDisabilityId:34,
        primaryDisabilityText:"Hearing Impairment (HI)",
        productivityMeasure:0.1,
        totalHours:0.1,
        workAtOtherSite:false,
        workType:"xcv"
      };

      sectionWorkSitesController = function() {
        return $controller('sectionWorkSitesController', {
          $scope: scope,
          $route: route
        });
      };
    })
  );

  it('sectionWorkSitesController has clearActiveWorker', function() {
    var controller = sectionWorkSitesController();
    controller.clearActiveWorker();

    expect(controller.activeWorker).toEqual({});
    expect(controller.activeWorkerIndex).toBe(-1);
  });

  it('sectionWorkSitesController has addEmployee', function() {
    var controller = sectionWorkSitesController();
    controller.activeWorker = scope.worker
    spyOn(controller, 'clearActiveWorker');
    controller.addEmployee();

    expect(controller.activeWorksite.employees.length).toBe(1);
    expect(controller.activeWorksite.employees[0]).toBe(scope.worker);
    expect(controller.clearActiveWorker).toHaveBeenCalled();
  });

  it('sectionWorkSitesController has doneAddingEmployees', function() {
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    var controller = sectionWorkSitesController();
    controller.doneAddingEmployees(e);

    expect(controller.addingEmployee).toBe(false);
  });

  it('sectionWorkSitesController has editEmployee', function() {
    var e = jasmine.createSpyObj('e', ['preventDefault']);
    var controller = sectionWorkSitesController();
    controller.activeWorksite = { employees: [scope.worker] };
    controller.editEmployee(0, e);

    expect(controller.activeWorker.workType).toBe(scope.worker.workType);
    expect(controller.addingEmployee).toBe(true);
  });

  it('sectionWorkSitesController has addWorkSite', function() {
    var controller = sectionWorkSitesController();
    spyOn(controller, 'setActiveTab');
    controller.addWorkSite();

    expect(controller.addingWorkSite).toBe(true);
    expect(controller.setActiveTab).toHaveBeenCalledWith(1);
  });

  it('sectionWorkSitesController has saveWorkSite', function() {
    var controller = sectionWorkSitesController();
    spyOn(controller, 'clearActiveWorker');
    spyOn(controller, 'clearActiveWorkSite');
    controller.activeWorksite = { id: 1 };
    controller.saveWorkSite();

    expect(controller.clearActiveWorker).toHaveBeenCalled();
    expect(controller.clearActiveWorkSite).toHaveBeenCalled();
    expect(controller.addingWorkSite).toBe(false);
  });

  it('sectionWorkSitesController has editWorkSite', function() {
    var controller = sectionWorkSitesController();
    scope.formData.workSites = [{ id: 1 }];
    spyOn(controller, 'setActiveTab');
    controller.editWorkSite(0);
    expect(controller.addingWorkSite).toBe(true);
    expect(controller.setActiveTab).toHaveBeenCalledWith(1);
  });

  it('sectionWorkSitesController has deleteWorkSite', function() {
    var controller = sectionWorkSitesController();
    scope.formData.workSites = [{ id: 1 }];
    controller.deleteWorkSite(0);

    expect(scope.formData.workSites.length).toBe(0);
  });

  it('sectionWorkSitesController has setActiveTab', function() {
    var controller = sectionWorkSitesController();
    controller.setActiveTab(1);

    expect(controller.activeTab).toBe(1);
  });

  it('sectionWorkSitesController has getDisabilityDisplay, no employee passed', function() {
    var controller = sectionWorkSitesController();
    var results = controller.getDisabilityDisplay();

    expect(results).toBe(undefined);
  });

  //TODO getDisabilityDisplay assertions

  it('sectionWorkSitesController has validateActiveWorksiteProperty', function() {
    var controller = sectionWorkSitesController();
    spyOn(mockValidationService, 'getValidationErrors');
    controller.activeWorksiteIndex = 0;
    controller.validateActiveWorksiteProperty(1);

    expect(mockValidationService.getValidationErrors).toHaveBeenCalledWith(
      'workSites[0].1'
    );
  });

  it('sectionWorkSitesController has validateActiveWorkerProperty', function() {
    var controller = sectionWorkSitesController();
    spyOn(mockValidationService, 'getValidationErrors');
    controller.activeWorksiteIndex = 0;
    controller.activeWorkerIndex = 0;
    controller.validateActiveWorkerProperty(1);

    expect(mockValidationService.getValidationErrors).toHaveBeenCalledWith(
      'workSites[0].employees[0].1'
    );
  });

  it('sectionWorkSitesController has validateActiveWorksiteWorker', function() {
    var controller = sectionWorkSitesController();
    spyOn(mockValidationService, 'getValidationErrors');
    controller.activeWorksiteIndex = 0;
    controller.validateActiveWorksiteWorker(1);

    expect(mockValidationService.getValidationErrors).toHaveBeenCalledWith(
      'workSites[0].employees[1]'
    );
  });

  it('should toggle example view', function() {
    var controller = sectionWorkSitesController();
    var HTMLElements = {};
    document.getElementById = jasmine.createSpy('HTML Element').and.callFake(function(ID) {
       if(!HTMLElements[ID]) {
          var newElement = document.createElement('div');
          newElement.id = 'exampleFirstFocus';
          HTMLElements[ID] = newElement;
       }
       return HTMLElements[ID];
    });;

    controller.exampleView = '1';
    controller.showExampleView('2');
    expect(controller.exampleView).toBe('2');
  });

  // test add/edit/remove workers and worksites
  it('should add/edit/delete a worker and a worksite', function() {
    var controller = sectionWorkSitesController();
    var e = jasmine.createSpyObj('e', ['preventDefault']);

    controller.activeWorksite = {};
    controller.activeWorker = {
      name: 'First Last',
      primaryDisability: 31
    };

    controller.getDisabilityDisplay(controller.activeWorker);

    controller.addAnotherEmployee();
    controller.doneAddingEmployees(e);
    expect(controller.activeWorksite.employees.length).toBe(2);

    controller.editEmployee(0, e);
    controller.activeWorker.primaryDisability = 38;
    controller.activeWorker.primaryDisabilityId = 38;
    controller.activeWorker.primaryDisabilityOther = 'other';

    controller.getDisabilityDisplay(controller.activeWorker);

    controller.addEmployee();
    expect(controller.activeWorksite.employees.length).toBe(2);

    controller.deleteEmployee(0, e);
    expect(controller.activeWorksite.employees.length).toBe(1);

    controller.saveWorkSite();
    expect(scope.formData.workSites.length).toBe(1);

    controller.editWorkSite(0);
    controller.addWorkSite();
    expect(scope.formData.workSites.length).toBe(1);

    controller.deleteWorkSite(0);
    expect(scope.formData.workSites.length).toBe(0);
  });
});
