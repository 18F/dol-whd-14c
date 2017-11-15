describe('landingPageController', function() {
  var scope, landingPageController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      landingPageController = function() {
        return $controller('landingPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    landingPageController();
  });
});
