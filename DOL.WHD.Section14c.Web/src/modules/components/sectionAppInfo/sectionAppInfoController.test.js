describe('sectionAppInfoController', function() {
  var scope, route, mockResponsesService, mockNavService;
  var sectionAppInfoController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route, responsesService, navService) {
      scope = $rootScope.$new();
      route = $route;
      mockResponsesService = responsesService;
      mockNavService = navService;

      sectionAppInfoController = function() {
        return $controller('sectionAppInfoController', {
          $scope: scope,
          navService: mockNavService,
          $route: route,
          responsesService: mockResponsesService
        });
      };
    })
  );

  it('toggleEstablishmentType toggle on', function() {
    var controller = sectionAppInfoController();
    scope.formData.establishmentTypeId = [];
    controller.toggleEstablishmentType(1);

    expect(scope.formData.establishmentTypeId.indexOf(1)).toBe(0);
  });

  it('toggleEstablishmentType toggle off', function() {
    var controller = sectionAppInfoController();
    scope.formData.establishmentTypeId = [1];
    controller.toggleEstablishmentType(1);

    expect(scope.formData.establishmentTypeId.indexOf(1)).toBe(-1);
  });

  it('toggle all help text will toggle on', function() {
    var controller = sectionAppInfoController();
    scope.showAllHelp.status = false;
    controller.toggleAllHelpText({srcElement: {id: 'test'}});
    expect(scope.showAllHelp.status).toBe(true)
  });

  it('toggle all help text will toggle on', function() {
    var controller = sectionAppInfoController();
    scope.showAllHelp.status = true;
    controller.toggleAllHelpText({srcElement: {id: 'test'}});
    expect(scope.showAllHelp.status).toBe(false)
  });
});
