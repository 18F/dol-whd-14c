describe('dolHeaderController', function() {
  var dolHeaderController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller
    ) {
      var scope = $rootScope.$new();

      dolHeaderController = function() {
        return $controller('dolHeaderController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = dolHeaderController();
    expect(controller).toBeDefined();
  });
});
