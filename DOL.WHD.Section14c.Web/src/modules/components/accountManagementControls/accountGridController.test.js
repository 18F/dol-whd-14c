describe('accountGridController', function() {
  var scope, $q, mockApiService, mockLocation;
  var accountGridController, getAccounts;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService, $location) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;
      mockLocation = $location;

      accountGridController = function() {
        return $controller('accountGridController', {
          $scope: scope,
          apiService: mockApiService
        });
      };

      getAccounts = $q.defer();
      spyOn(mockApiService, 'getAccounts').and.returnValue(getAccounts.promise);
    })
  );

  it('invoke controller', function() {
    var controller = accountGridController();
    expect(controller).toBeDefined();
  });

  it('accounts load', function() {
    accountGridController();
    getAccounts.resolve({ data: [{ userId: 1 }] });
    scope.$apply();

    expect(scope.accounts.length).toBe(1);
    expect(scope.accounts[0].userId).toBe(1);
  });

  it('accounts loading failure displays an error, error description', function() {
    var controller = accountGridController();
    getAccounts.reject({ data: { error: {} } });
    scope.$apply();
    expect(controller.loadingError).toBe(true);
  });

  it('edit account click navigates to /account/{userId}', function() {
    var controller = accountGridController();
    spyOn(mockLocation, 'path');
    controller.editAccountClick(1);
    expect(mockLocation.path).toHaveBeenCalledWith('/account/1');
  });
});
