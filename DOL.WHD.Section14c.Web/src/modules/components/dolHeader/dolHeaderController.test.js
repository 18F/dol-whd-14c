describe('dolHeaderController', function() {
  var scope, dolHeaderController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller
    ) {
      scope = $rootScope.$new();

      dolHeaderController = function() {
        return $controller('dolHeaderController', {
          $scope: scope
        });
      };
    })
  );
});
