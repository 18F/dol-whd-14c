describe('helpPageController', function() {
  var scope, helpPageController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      helpPageController = function() {
        return $controller('helpPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = helpPageController();
    expect(controller).toBeDefined();
  });
});
