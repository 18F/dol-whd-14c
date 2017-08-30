describe('accountGrid', function() {
  beforeEach(module('14c'));

  var element, rootScope;
  beforeEach(function() {
    element = angular.element('<account-grid/>');
    inject(function($rootScope, $compile, _$q_, apiService) {
      $q = _$q_;
      rootScope = $rootScope;
      responses = $q.defer();
      mockApiService = apiService;
      spyOn(mockApiService, 'getAccounts').and.returnValue(responses.promise);
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});
