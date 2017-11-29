describe('accountGrid', function() {
  var element, rootScope;

  beforeEach(module('14c'));

  beforeEach(function() {
    element = angular.element('<account-grid/>');
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
