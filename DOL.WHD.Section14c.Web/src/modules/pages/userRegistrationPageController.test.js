describe('userRegistrationPageController', function() {
  var scope, userRegistrationPageController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      userRegistrationPageController = function() {
        return $controller('userRegistrationPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = userRegistrationPageController();
    expect(controller).toBeDefined();
  });
});
