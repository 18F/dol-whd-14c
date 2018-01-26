describe('sectionEmployerController', function() {
  var scope, route, mockNavService, sectionEmployerController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $route, navService) {
      scope = $rootScope.$new();
      route = $route;
      mockNavService = navService;

      sectionEmployerController = function() {
        return $controller('sectionEmployerController', {
          $scope: scope,
          navService: mockNavService,
          $route: route
        });
      };
    })
  );

  it('onHasTradeNameChange', function() {
    var controller = sectionEmployerController();
    scope.formData.employer.tradeName = 'value';
    controller.onHasTradeNameChange();

    expect(scope.formData.employer.tradeName).toBe('');
  });

  it('has n/a for county when DC is selected as state', function() {
    var controller = sectionEmployerController();

    scope.formData.employer.physicalAddress = {};
    scope.formData.employer.physicalAddress.state = 'DC';
    scope.$digest();
    expect(scope.formData.employer.physicalAddress.county).toBe('N/A');
  });

  it('county is undefined when PA is selected as state', function() {
    var controller = sectionEmployerController();

    scope.formData.employer.physicalAddress = {};
    scope.formData.employer.physicalAddress.state = 'PA';
    scope.$digest();
    expect(scope.formData.employer.physicalAddress.county).toBe(undefined);
  });

  it('onHasLegalNameChange', function() {
    var controller = sectionEmployerController();
    scope.formData.employer.priorLegalName = 'value';
    controller.onHasLegalNameChange();

    expect(scope.formData.employer.priorLegalName).toBe('');
  });

  it('toggleDeductionType toggle on', function() {
    var controller = sectionEmployerController();
    scope.formData.employer.providingFacilitiesDeductionTypeId = [];
    controller.toggleDeductionType(1);

    expect(
      scope.formData.employer.providingFacilitiesDeductionTypeId.indexOf(1)
    ).toBe(0);
  });

  it('toggleDeductionType toggle off', function() {
    var controller = sectionEmployerController();
    scope.formData.employer.providingFacilitiesDeductionTypeId = [1];
    controller.toggleDeductionType(1);

    expect(
      scope.formData.employer.providingFacilitiesDeductionTypeId.indexOf(1)
    ).toBe(-1);
  });

  it('toggle all help text will toggle on', function() {
    var controller = sectionEmployerController();
    scope.showAllHelp.status = false;
    controller.toggleAllHelpText({srcElement: {id: 'test'}});
    expect(scope.showAllHelp.status).toBe(true)
  });

  it('toggle all help text will toggle on', function() {
    var controller = sectionEmployerController();
    scope.showAllHelp.status = true;
    controller.toggleAllHelpText({srcElement: {id: 'test'}});
    expect(scope.showAllHelp.status).toBe(false)
  });
});
