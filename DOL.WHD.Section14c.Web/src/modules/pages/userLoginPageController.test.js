describe('userLoginPageController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      userLoginPageController = function() {
        return $controller('userLoginPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = userLoginPageController();
  });
});
