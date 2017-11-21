describe('forgotPasswordPageController', function() {
  var scope, forgotPasswordPageController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      forgotPasswordPageController = function() {
        return $controller('forgotPasswordPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    forgotPasswordPageController();
  });
});
