describe('changePasswordPageController', function() {
  var scope, changePasswordPageController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      changePasswordPageController = function() {
        return $controller('changePasswordPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    changePasswordPageController();
  });
});
