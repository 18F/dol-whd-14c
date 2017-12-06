describe('formSection', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<form-section/>');
    inject(function($rootScope) {
      rootScope = $rootScope;
      //$compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});

describe('helplink', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<helplink/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });

  it('when clicked, toggles expanded to false when true', function() {
    rootScope.$digest();
    rootScope.expanded = true;
    element.trigger('click');
    expect(rootScope.expanded).toBe(false);
  });

  it('when clicked, toggles expanded to true when false', function() {
    rootScope.$digest();
    rootScope.expanded = false;
    element.trigger('click');
    expect(rootScope.expanded).toBe(true);
  });
});

describe('helptext', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<helptext/>');
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
