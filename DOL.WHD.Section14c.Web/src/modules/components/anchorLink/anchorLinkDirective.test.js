describe('anchorLink', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<anchor-link/>');
    inject(function($rootScope) {
      rootScope = $rootScope;
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});
