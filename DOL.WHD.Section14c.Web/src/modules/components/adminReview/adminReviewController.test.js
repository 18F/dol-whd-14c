describe('adminReviewController', function() {
  var scope, stateServiceMock;
  var adminReviewController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, stateService) {
      scope = $rootScope.$new();
      stateServiceMock = stateService;

      adminReviewController = function() {
        return $controller('adminReviewController', {
          $scope: scope,
          stateService: stateServiceMock,
          _constants: { }
        });
      };
    })
  );

  describe('constructor/initializer', function() {
    it('does not load application if app ID is not set', function() {
      adminReviewController();
    });

    it('does load application if app ID is set', function() {
      scope.appid = 'app-id';
      spyOn(stateServiceMock, 'loadApplicationData');

      adminReviewController();

      expect(stateServiceMock.loadApplicationData).toHaveBeenCalledWith('app-id');
    });
  });
});
