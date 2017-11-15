describe('sectionAssurancesController', function() {
  var scope, sectionAssurancesController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

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
