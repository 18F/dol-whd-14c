describe('mainNavigationControlController', function() {
  var scope, route, mockNavService;
  var mainNavigationControlController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route, navService) {
      scope = $rootScope.$new();
      route = $route;
      mockNavService = navService;

      spyOn(mockNavService, 'hasNext');
      spyOn(mockNavService, 'hasBack');

      route.current = {
        params: { section_id: 1 }
      };

      spyOn(mockNavService, 'getNextSection');

      mainNavigationControlController = function() {
        return $controller('mainNavigationControlController', {
          $scope: scope,
          navService: mockNavService,
          $route: route
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = mainNavigationControlController();
    spyOn(mockNavService, 'gotoSection');
    var event = { target: { dataset: { sectionid: 'wioa' } } };
    controller.onNavClick(event);

    expect(mockNavService.gotoSection).toHaveBeenCalledWith('wioa');
  });

  describe('sets the document title correctly', function() {
    var controller, event;
    beforeEach(function() {
      controller = mainNavigationControlController();
      spyOn(mockNavService, 'gotoSection');
      event = { target: { dataset: { sectionid: '' } } };
    });
  
    it('sets the title for app-info', function() {
      event.target.dataset.sectionid = 'app-info';
      controller.onNavClick(event);
      expect(document.title).toBe('Application Info | DOL WHD Section 14(c)');
    });

    it('sets the title for work-sites', function() {
      event.target.dataset.sectionid = 'work-sites';
      controller.onNavClick(event);
      expect(document.title).toBe('Work Sites & Employees | DOL WHD Section 14(c)');
    });

    it('sets the title for wioa', function() {
      event.target.dataset.sectionid = 'wioa';
      controller.onNavClick(event);
      expect(document.title).toBe('WIOA | DOL WHD Section 14(c)');
    });

    it('sets the title for unhandled-case', function() {
      event.target.dataset.sectionid = 'unhandled-case';
      controller.onNavClick(event);
      expect(document.title).toBe('Unhandled Case | DOL WHD Section 14(c)');
    });
  });
});


