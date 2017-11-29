describe('userRegistrationForm', function() {
  var element, rootScope, scope;
  beforeEach(module('14c'));

  beforeEach(function() {
    element = angular.element('<user-registration-form/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(element)(rootScope);
      scope = $rootScope.$new();
    });
  });

  it('invoke directive', function() {
    scope.model = { key: 'key' };
    scope.$digest();
    expect(element).toBeDefined();
  });
});
