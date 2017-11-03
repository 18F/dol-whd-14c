describe('dolHeaderController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller,
      $timeout,
      $window
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
