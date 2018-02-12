describe('focusOn', function() {
  beforeEach(module('14c'));


  var element, rootScope, compiled;
  beforeEach(function() {
    element = angular.element('<input focus-on="true"/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      compiled = $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
    expect(element.attr('focus-on')).toEqual("true");
  });
});
