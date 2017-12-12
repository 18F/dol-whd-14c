describe('dashboardController', function() {
  var scope, dashboardController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      dashboardController = function() {
        return $controller('dashboardController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = dashboardController();
    expect(controller).toBeDefined();
  });
});
