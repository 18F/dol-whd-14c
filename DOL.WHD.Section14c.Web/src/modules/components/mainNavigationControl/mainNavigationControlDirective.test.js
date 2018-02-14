describe('mainNavigationControl', function() {
  beforeEach(module('14c'));

  var element, rootScope, controller, mockNavService, mainNavigationControlController;

  beforeEach(function() {
    element = angular.element('<main-navigation-control/>');
    inject(function($rootScope, $compile, $document, navService, $controller) {
      rootScope = $rootScope.$new();
      controller = $controller
      mockNavService = navService;
      $compile(element)(rootScope);
      spyOn(mockNavService, 'gotoSection');
      mainNavigationControlController = function() {
        return controller('mainNavigationControlController', {
          $scope: $rootScope,
          navService: mockNavService
        });
      };
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });

  it('navs to section on keydown', function() {
    rootScope.$digest();
    expect(rootScope.clicked).toBeFalsy();
    mainNavigationControlController();
    var target = {
      dataset: {
        sectionid: 'work-sites'
      }
    }
    element.triggerHandler({ type: 'keydown', keyCode: 13, target: target});
    expect(mockNavService.gotoSection).toHaveBeenCalled();
  });
});
