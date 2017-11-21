describe('wageDataPayTypeForm', function() {
  var element, rootScope;

  beforeEach(module('14c'));

  beforeEach(function() {
    element = angular.element('<wage-data-pay-type-form/>');
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
