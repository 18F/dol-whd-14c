describe('adminReviewDirective', function() {
  beforeEach(module('14c'));

  var adminReview, rootScope;
  beforeEach(function() {
    adminReview = angular.element('<admin-review/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(adminReview)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(adminReview).toBeDefined();
  });
});
