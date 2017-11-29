describe('accountPageController', function() {
  var scope, accountPageController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      accountPageController = function() {
        return $controller('accountPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = accountPageController();
    expect(controller).toBeDefined();
  });
});
