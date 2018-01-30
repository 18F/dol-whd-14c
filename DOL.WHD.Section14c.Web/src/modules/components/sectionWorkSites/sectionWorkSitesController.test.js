describe('sectionWorkSitesController', function() {
  var rootScope, location, scope, route, mockDocument, mockValidationService, sectionWorkSitesController, mockNavService;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $location, $controller, $route, $document, validationService, navService) {
      rootScope = $rootScope;
      location = $location;
      scope = $rootScope.$new();
      route = $route;
      mockDocument = $document;
      mockValidationService = validationService;
      mockNavService = navService;
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

  it('clearActiveWorker clears the active worker', function() {
    var controller = sectionWorkSitesController();
    controller.clearActiveWorker();

    expect(controller.activeWorker).toEqual({});
    expect(controller.activeWorkerIndex).toBe(-1);
  });

  it('cancel and close pane clears the active worker', function() {
    var e = jasmine.createSpyObj('e', ['preventDefault']);

    var controller = sectionWorkSitesController();
    spyOn(controller, 'clearActiveWorker');
    spyOn(controller, 'addEmployee');
    controller.cancelAndClosePanel(e);

    expect(controller.activeWorker).toEqual({});
    expect(controller.activeWorkerIndex).toBe(-1);
    expect(controller.clearActiveWorker).toHaveBeenCalled();
    expect(controller.addEmployee).not.toHaveBeenCalled();
  });

  describe('addEmployee', function() {
    var controller;

    beforeEach(function() {
      controller = sectionWorkSitesController();
      spyOn(controller, 'clearActiveWorker');
    });

    it('bails out if there is no active worksite', function() {
      controller.activeWorksite = null;
      controller.addEmployee();

      expect(controller.clearActiveWorker).not.toHaveBeenCalled();
    });

    it('adds employee if everything is fine', function() {
      controller.activeWorker = scope.worker
      controller.addEmployee();

      expect(controller.activeWorksite.employees.length).toBe(1);
      expect(controller.activeWorksite.employees[0]).toBe(scope.worker);
      expect(controller.clearActiveWorker).toHaveBeenCalled();
    });
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

  describe('saveWorkSite', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
      spyOn(controller, 'clearActiveWorker');
      spyOn(controller, 'clearActiveWorkSite');
    });

    it('does nothing if the active worksite is empty', function() {
      controller.activeWorksite = null;
      controller.saveWorkSite();

      expect(scope.formData.workSites.length).toBe(0);
      expect(controller.clearActiveWorker).toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).toHaveBeenCalled();
      expect(controller.addingWorkSite).toBe(false);
    });

    it('replaces the active worksite if set', function() {
      var worksite = { id: 1 };
      controller.activeWorksite = worksite;
      controller.activeWorksiteIndex = 0;
      scope.formData.workSites = [{ id: 10 }, { id: 20}];
      controller.saveWorkSite();

      expect(scope.formData.workSites.length).toBe(2);
      expect(scope.formData.workSites[0]).toBe(worksite);
      expect(controller.clearActiveWorker).toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).toHaveBeenCalled();
      expect(controller.addingWorkSite).toBe(false);
    });

    it('adds the work site if the active worksite index is not negative', function() {
      var worksite = { id: 1 };
      controller.activeWorksite = worksite;
      controller.saveWorkSite();

      expect(scope.formData.workSites.length).toBe(1);
      expect(scope.formData.workSites[0]).toBe(worksite);
      expect(controller.clearActiveWorker).toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).toHaveBeenCalled();
      expect(controller.addingWorkSite).toBe(false);
    });
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

  describe('setActiveTab', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
      spyOn(mockNavService, 'setNextQuery');
      spyOn(mockNavService, 'setBackQuery');
    });

    it('does nothing if not adding a work site', function() {
      controller.addingWorkSite = false;
      controller.setActiveTab(2);

      expect(controller.activeTab).toBe(1);
      expect(mockNavService.setNextQuery).not.toHaveBeenCalled();
      expect(mockNavService.setBackQuery).not.toHaveBeenCalled();
    });

    it('does nothing if selecting a tab less than 1', function() {
      controller.addingWorkSite = false;
      controller.setActiveTab(0);

      expect(controller.activeTab).toBe(1);
      expect(mockNavService.setNextQuery).not.toHaveBeenCalled();
      expect(mockNavService.setBackQuery).not.toHaveBeenCalled();
    });

    it('does nothing if selecting a tab greater than 2', function() {
      controller.addingWorkSite = false;
      controller.setActiveTab(5);

      expect(controller.activeTab).toBe(1);
      expect(mockNavService.setNextQuery).not.toHaveBeenCalled();
      expect(mockNavService.setBackQuery).not.toHaveBeenCalled();
    });

    it('sets the active tab and updates navigation for tab 1', function() {
      controller.addingWorkSite = true;
      controller.setActiveTab(1);

      expect(controller.activeTab).toBe(1);
      expect(mockNavService.setNextQuery).toHaveBeenCalledWith({ t: 2}, 'Next: Add Employee(s)', 'worksite_tab_box');
      expect(mockNavService.setBackQuery).toHaveBeenCalledWith({ doCancel: true }, 'Cancel');
    });

    it('sets the active tab and updates navigation for tab 2', function() {
      controller.addingWorkSite = true;
      controller.setActiveTab(2);

      expect(controller.activeTab).toBe(2);
      expect(mockNavService.setNextQuery).toHaveBeenCalledWith({ doSave: true}, 'Save Work Site & Employee(s)');
      expect(mockNavService.setBackQuery).toHaveBeenCalledWith({ doCancel: true }, 'Cancel');
    });
  });

  describe('getDisabilityDisplay', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
    });

    it('returns undefined for no defined employee', function() {
      var results = controller.getDisabilityDisplay();
      expect(results).toBe(undefined);
    });

    it('returns undefined for an unknown disability ID', function() {
      var results = controller.getDisabilityDisplay({ primaryDisabilityId: 0 });
      expect(results).toBe(undefined);
    });

    describe('returns a string for a known disability ID', function() {
      beforeEach(function() {
        scope.responses.PrimaryDisability = [{
          id: 10,
          display: 'Display text'
        }];
      });

      it('returns the alternate key text from the employee, if defined', function() {
        scope.responses.PrimaryDisability[0].otherValueKey = 'otherkey';
        var result = controller.getDisabilityDisplay({ primaryDisabilityId: 10, otherkey: 'Alternate key text' });
        expect(result).toBe('Alternate key text');
      });

      it('returns the disability display key if there is not an alternate key', function() {
        var result = controller.getDisabilityDisplay({ primaryDisabilityId: 10 });
        expect(result).toBe('Display text');
      });
    });
  });

  describe('workerProductivityChanged', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
    });

    it('does not change the productivity if the study is truthy', function() {
      controller.activeWorker.productivityMeasure = 17;
      controller.workerProductivityChanged(true);

      expect(controller.activeWorker.productivityMeasure).toBe(17);
    });

    it('does not change the productivity if there is no active worker', function() {
      controller.activeWorker = null;
      controller.workerProductivityChanged();

      expect(controller.activeWorker).toBe(null);
    });

    it('unsets the productivity if study is falsey and there is an active worker', function() {
      controller.workerProductivityChanged();
      expect(controller.activeWorker.productivityMeasure).toBe(undefined);
    });
  });

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

  describe('validateActiveWorksiteWorker', function() {
    it('validateActiveWorksiteWorker returns undefined for invalid index', function() {
      var controller = sectionWorkSitesController();

      controller.activeWorksiteIndex = 0;
      var actual = controller.validateActiveWorksiteWorker(-1);

      expect(actual).toBe(undefined);
    });

    it('validateActiveWorksiteWorker returns undefined for invalid worksite index', function() {
      var controller = sectionWorkSitesController();

      controller.activeWorksiteIndex = -1;
      var actual = controller.validateActiveWorksiteWorker(1);

      expect(actual).toBe(undefined);
    });

    it('validateActiveWorksiteWorker calls the validation service on valid indices', function() {
      var controller = sectionWorkSitesController();
      spyOn(mockValidationService, 'getValidationErrors');
      controller.activeWorksiteIndex = 0;
      controller.validateActiveWorksiteWorker(1);

      expect(mockValidationService.getValidationErrors).toHaveBeenCalledWith(
        'workSites[0].employees[1]'
      );
    });
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
    });

    controller.exampleView = '1';
    controller.showExampleView('2');
    expect(controller.exampleView).toBe('2');
  });

  describe('toggleAllHelpText', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
    });

    it('will toggle off if already on', function() {
      scope.showAllHelp.status = true;
      controller.toggleAllHelpText({srcElement: {id: 'test'}});
      expect(scope.showAllHelp.status).toBe(false);
    });

    it('will toggle on if already off', function() {
      scope.showAllHelp.status = false;
      controller.toggleAllHelpText({srcElement: {id: 'test'}});
      expect(scope.showAllHelp.status).toBe(true);
    });
  });

  describe('tabPanelFocus', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
      // spyOn(mockDocument[0], 'getElementById');
    });

    it('will handle switching to tab 1', function() {
      controller.tabPanelFocus(1);
      expect(mockDocument[0].getElementById).toHaveBeenCalledWith('worksitesTabPanel');
    });

    it('will handle switching to other tabs', function() {
      controller.tabPanelFocus(15);
      expect(mockDocument[0].getElementById).toHaveBeenCalledWith('employeesTabPanel');
    });
  });

  describe('$routeUpdate handler', function() {
    var controller;
    beforeEach(function() {
      controller = sectionWorkSitesController();
      spyOn(controller, 'setActiveTab');
      spyOn(controller, 'saveWorkSite');
      spyOn(controller, 'clearActiveWorker');
      spyOn(controller, 'clearActiveWorkSite');
      spyOn(mockNavService, 'clearQuery');
    });

    it('does nothing if the location search does not return anything we understand', function() {
      spyOn(location, 'search').and.returnValue({ });
      rootScope.$broadcast('$routeUpdate');

      expect(controller.setActiveTab).not.toHaveBeenCalled();
      expect(controller.saveWorkSite).not.toHaveBeenCalled();
      expect(controller.clearActiveWorker).not.toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).not.toHaveBeenCalled();
      expect(mockNavService.clearQuery).not.toHaveBeenCalled();
    });

    it('sets the active tab if the location search returns a tab ID', function() {
      spyOn(location, 'search').and.returnValue({ t: 7 });
      rootScope.$broadcast('$routeUpdate');

      expect(controller.setActiveTab).toHaveBeenCalledWith(7);
      expect(controller.saveWorkSite).not.toHaveBeenCalled();
      expect(controller.clearActiveWorker).not.toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).not.toHaveBeenCalled();
      expect(mockNavService.clearQuery).not.toHaveBeenCalled();
    });

    it('saves the worksite and clears the query if the location search returns doSave', function() {
      spyOn(location, 'search').and.returnValue({ doSave: true });
      rootScope.$broadcast('$routeUpdate');

      expect(controller.setActiveTab).not.toHaveBeenCalled();
      expect(controller.saveWorkSite).toHaveBeenCalled();
      expect(controller.clearActiveWorker).not.toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).not.toHaveBeenCalled();
      expect(mockNavService.clearQuery).toHaveBeenCalled();
    });

    it('clears the active worker and worksite if the location search returns doCancel', function() {
      spyOn(location, 'search').and.returnValue({ doCancel: true });
      rootScope.$broadcast('$routeUpdate');

      expect(controller.setActiveTab).not.toHaveBeenCalled();
      expect(controller.saveWorkSite).not.toHaveBeenCalled();
      expect(controller.clearActiveWorker).toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).toHaveBeenCalled();
      expect(mockNavService.clearQuery).toHaveBeenCalled();
    });

    it('does all the things if a tab ID, doSave, and doCancel are all set', function() {
      spyOn(location, 'search').and.returnValue({ t: 7, doSave: true, doCancel: true });
      rootScope.$broadcast('$routeUpdate');

      expect(controller.setActiveTab).toHaveBeenCalledWith(7);
      expect(controller.saveWorkSite).toHaveBeenCalled();
      expect(controller.clearActiveWorker).toHaveBeenCalled();
      expect(controller.clearActiveWorkSite).toHaveBeenCalled();
      expect(mockNavService.clearQuery).toHaveBeenCalled();
    });
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
