describe('sectionWioa', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<section-wioa/>');
    inject(function($rootScope, $compile, responsesService) {
      rootScope = $rootScope;
      mockResponsesService = responsesService;
      spyOn(mockResponsesService, 'getQuestionResponses').and.returnValue(
        responses.promise
      );
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});
