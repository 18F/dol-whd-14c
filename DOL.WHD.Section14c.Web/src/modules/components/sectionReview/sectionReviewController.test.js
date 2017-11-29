describe('sectionReviewController', function() {
  var scope, route, q, mockLocation;
  var mockApiService, mockNavService;
  var sectionReviewController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $location, $controller, apiService, $q, $route, navService) {
      scope = $rootScope.$new();
      mockLocation = $location;
      mockApiService = apiService;
      mockNavService = navService;
      route = $route;
      q = $q;

      sectionReviewController = function() {
        return $controller('sectionReviewController', {
          $scope: scope,
          $location: mockLocation,
          navService: mockNavService,
          $route: route
        });
      };
    })
  );

  it('submitApplication invalid form', function() {
    var controller = sectionReviewController();
    spyOn(mockApiService, 'submitApplication');
    scope.isValid = false;
    controller.onSubmit();

    expect(mockApiService.submitApplication).not.toHaveBeenCalled();
  });

  it('submitApplication valid form', function() {
    var controller = sectionReviewController();
    var d = q.defer();
    spyOn(mockApiService, 'submitApplication').and.returnValue(d.promise);

    scope.isValid = true;
    controller.onSubmit();
    d.resolve();
    scope.$apply();

    expect(controller.submissionSuccess).toBe(true);
  });
});
