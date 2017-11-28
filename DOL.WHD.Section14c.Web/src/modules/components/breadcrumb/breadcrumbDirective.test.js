describe('breadcrumb', function() {
  beforeEach(module('14c'));

  var element, rootScope, _crumble, compile, location;
  beforeEach(function() {
    element = angular.element('<breadcrumb/>');
    inject(function($rootScope, $compile, crumble, $location) {
      rootScope = $rootScope;
      _crumble = crumble;
      compile = $compile;
      location = $location
      spyOn(_crumble, "update");
    });

  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });

  it('update crumble object', function() {
    location.path('/section/assurances');
    compile(element)(rootScope);
    rootScope.$digest();
    expect(_crumble.update).toHaveBeenCalled();
    expect(_crumble.context.name).toBe("Assurances");
  });



});
