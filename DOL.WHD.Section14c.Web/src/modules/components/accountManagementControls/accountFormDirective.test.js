describe('accountForm', function() {
  var element, $q, rootScope, mockApiService;
  var getAccount, getRoles;

  beforeEach(module('14c'));

  beforeEach(function() {
    element = angular.element('<account-form/>');
    inject(function($rootScope, $compile, _$q_, apiService) {
      $q = _$q_;
      rootScope = $rootScope;
      mockApiService = apiService;

      getAccount = $q.defer();
      spyOn(mockApiService, 'getAccount').and.returnValue(getAccount.promise);

      getRoles = $q.defer();
      spyOn(mockApiService, 'getRoles').and.returnValue(getRoles.promise);

      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });
});
