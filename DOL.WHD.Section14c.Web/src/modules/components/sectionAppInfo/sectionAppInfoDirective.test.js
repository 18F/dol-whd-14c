describe('sectionAppInfo', function() {
  var element, $q, rootScope, responses;
  var mockResponsesService;

  beforeEach(module('14c'));

  beforeEach(function() {
    element = angular.element('<section-app-info/>');
    inject(function($rootScope, $compile, _$q_, responsesService) {
      $q = _$q_;
      rootScope = $rootScope;
      responses = $q.defer();
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
