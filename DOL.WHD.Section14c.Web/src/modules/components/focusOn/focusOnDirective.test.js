describe('focusOn', function() {
  beforeEach(module('14c'));

  var element, rootScope;

  beforeEach(function() {
    element = angular.element('<input focus-on="true"/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
    expect(element.attr('focus-on')).toEqual("true");
  });
});
