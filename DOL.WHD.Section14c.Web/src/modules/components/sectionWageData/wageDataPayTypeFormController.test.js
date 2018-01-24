describe('wageDataPayTypeFormController', function() {
  var scope, route, wageDataPayTypeFormController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route) {
      scope = $rootScope.$new();
      route = $route;

      wageDataPayTypeFormController = function() {
        return $controller('wageDataPayTypeFormController', {
          $scope: scope,
          $route: route
        });
      };
    })
  );

  // test add/edit/validate/remove a source employer
  it('should add/edit/validate/delete a source employer', function() {
    var controller = wageDataPayTypeFormController();
    controller.showAddEmployer();
    expect(controller.focusAddSourceEmployerForm).toBe(true);
    expect(controller.focusAddSourceEmployerButton).toBe(false);
    controller.activeSourceEmployer = { employerName: 'Employer 1' };
    controller.addSourceEmployer();
    expect(
      scope.formData[scope.modelPrefix()].mostRecentPrevailingWageSurvey
        .sourceEmployers.length
    ).toBe(1);
    expect(controller.focusAddSourceEmployerForm).toBe(false);
    expect(controller.focusAddSourceEmployerButton).toBe(true);
    controller.editSourceEmployer(0);
    controller.addSourceEmployer();
    expect(
      scope.formData[scope.modelPrefix()].mostRecentPrevailingWageSurvey
        .sourceEmployers.length
    ).toBe(1);

    controller.validateSourceEmployer(0);

    controller.deleteSourceEmployer(0);
    expect(
      scope.formData[scope.modelPrefix()].mostRecentPrevailingWageSurvey
        .sourceEmployers.length
    ).toBe(0);
    expect(controller.focusAddSourceEmployerForm).toBe(false);
    expect(controller.focusAddSourceEmployerButton).toEqual(1);
    controller.cancelAddSourceEmployer();
  });
});
