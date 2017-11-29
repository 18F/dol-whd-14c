describe('dolHeader', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<dol-header/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});
