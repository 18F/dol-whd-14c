describe('sectionAssurancesController', function() {
  var scope, route, mockStateService, mockNavService;
  var sectionAssurancesController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route, stateService, navService) {
      scope = $rootScope.$new();
      route = $route;
      mockStateService = stateService;
      mockNavService = navService;

      mockStateService.formData = 'value';

      sectionAssurancesController = function() {
        return $controller('sectionAssurancesController', {
          $scope: scope,
          navService: mockNavService,
          $route: route,
          stateService: mockStateService
        });
      };
    })
  );

  it('invoke controller, intialize formData from state service', function() {
    sectionAssurancesController();
    expect(scope.formData).toBe('value');
  });
});
