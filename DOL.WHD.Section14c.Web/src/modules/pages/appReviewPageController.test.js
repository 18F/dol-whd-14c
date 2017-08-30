describe('appReviewPageController', function() {
  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();

      appReviewPageController = function() {
        return $controller('appReviewPageController', {
          $scope: scope
        });
      };
    })
  );

  it('invoke controller', function() {
    var controller = appReviewPageController();
  });
});
