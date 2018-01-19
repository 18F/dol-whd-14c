describe('attachmentField', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<attachment-field/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      rootScope.inputId = "inputId";
      rootScope.modelPrefix = "modelPrefix";
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});
