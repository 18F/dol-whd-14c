describe('sectionWageDataController', function() {
  var rootScope, scope, route, sectionWageDataController, controller;
  var navServiceMock, documentMock, locationMock, stateServiceMock;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route, navService, $document, $location, stateService) {
      rootScope = $rootScope;
      scope = $rootScope.$new();
      route = $route;
      navServiceMock = navService;
      documentMock = $document;
      locationMock = $location;
      stateServiceMock = stateService;

      sectionWageDataController = function() {
        return $controller('sectionWageDataController', {
          $scope: scope,
          $route: route
        });
      };
    })
  );

  describe('constructor/initializer', function() {
    beforeEach(function() {
      spyOn(navServiceMock, 'clearNextQuery');
      spyOn(navServiceMock, 'goNext');
    });

    // Code smell - the constructor is doing conditional functionality.
    // The conditional should maybe happen in a lifecycle event hook?
    // Maybe $onInit/ngOnInit()?
    // https://angular.io/guide/lifecycle-hooks

    it('normal constructor', function() {
      sectionWageDataController();
      expect(navServiceMock.clearNextQuery).not.toHaveBeenCalled();
      expect(navServiceMock.goNext).not.toHaveBeenCalled();
    });

    it('clears the next query and moves forward if this is an initial application', function() {
      stateServiceMock.formData = { applicationTypeId: 1 };
      sectionWageDataController();
      expect(navServiceMock.clearNextQuery).toHaveBeenCalled();
      expect(navServiceMock.goNext).toHaveBeenCalled();
    });
  });

  describe('onTabClick', function() {
    it('sets the active tab and the next query', function() {
      controller = sectionWageDataController();
      spyOn(controller, 'setNextTabQuery');
      controller.onTabClick('hello');

      expect(controller.activeTab).toBe('hello');
      expect(controller.setNextTabQuery).toHaveBeenCalledWith('hello');
    });
  });

  describe('setNextTabQuery', function() {
    beforeEach(function() {
      controller = sectionWageDataController();
      spyOn(navServiceMock, 'setNextQuery');
      spyOn(navServiceMock, 'clearNextQuery');
    });

    it('sets the tab for ID 1', function() {
      controller.setNextTabQuery(1);
      expect(navServiceMock.setNextQuery).toHaveBeenCalledWith({ t: 2 }, 'Next: Add Piece Rate', 'wagedata_tab_box');
      expect(navServiceMock.clearNextQuery).not.toHaveBeenCalled();
    });

    it('sets the tab for IDs other than 1', function() {
      controller.setNextTabQuery(10);
      expect(navServiceMock.setNextQuery).not.toHaveBeenCalled();
      expect(navServiceMock.clearNextQuery).toHaveBeenCalled();
    });
  });

  describe('tabPanelFocus', function() {
    beforeEach(function() {
      controller = sectionWageDataController();
      spyOn(documentMock[0], 'getElementById').and.returnValue({ focus: function() { }});
    });

    it('focuses on the first tab when given ID 1', function() {
      controller.tabPanelFocus(1);
      expect(documentMock[0].getElementById).toHaveBeenCalledWith('hourlyTabPanel');
    });

    it('focuses on the other tab for other IDs', function() {
      controller.tabPanelFocus(0);
      expect(documentMock[0].getElementById).toHaveBeenCalledWith('pieceRateTabPanel');
      controller.tabPanelFocus(9);
      expect(documentMock[0].getElementById).toHaveBeenCalledWith('pieceRateTabPanel');
      controller.tabPanelFocus('hello');
      expect(documentMock[0].getElementById).toHaveBeenCalledWith('pieceRateTabPanel');
    });
  });

  describe('$routeUpdate handler', function() {
    beforeEach(function() {
      controller = sectionWageDataController();
      spyOn(controller, 'setNextTabQuery');
    });

    it('sets the active tab to the value returned by the location search', function() {
      spyOn(locationMock, 'search').and.returnValue({ t: 7 });
      rootScope.$broadcast('$routeUpdate');
      expect(controller.activeTab).toBe(7);
      expect(controller.setNextTabQuery).toHaveBeenCalledWith(7);
    });

    it('sets the active tab to 1 when the location search does not have one', function() {
      spyOn(locationMock, 'search').and.returnValue({ });
      rootScope.$broadcast('$routeUpdate');
      expect(controller.activeTab).toBe(1);
      expect(controller.setNextTabQuery).toHaveBeenCalledWith(1);
    });
  });

  describe('formData.payTypeId $watch', function() {
    beforeEach(function() {
      stateServiceMock.formData = { payTypeId: 0 };
      controller = sectionWageDataController();
      spyOn(controller, 'setNextTabQuery');
    });

    it('sets next tab query to 1 if the current pay type is 23 ("both") and the active tab is the hourly pay tab (1)', function() {
      controller.activeTab = 1;
      scope.formData.payTypeId = 23;
      scope.$digest();

      expect(controller.setNextTabQuery).toHaveBeenCalledWith(1);
    });

    it('sets the next tab query to default if the current pay type is not 23 ("both")', function() {
      controller.activeTab = 1;
      scope.formData.payTypeId = 2;
      scope.$digest();

      expect(controller.setNextTabQuery).toHaveBeenCalledWith();
    });

    it('sets the next tab query to default if the active tab is not the hourly pay tab (1)', function() {
      controller.activeTab = 2;
      scope.formData.payTypeId = 23;
      scope.$digest();

      expect(controller.setNextTabQuery).toHaveBeenCalledWith();
    });
  });

  describe('toggleAllHelpText', function() {
    beforeEach(function() {
      controller = sectionWageDataController();
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

  //TODO test rest of controller
});
