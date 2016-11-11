describe('accountGridController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, _$q_, apiService, $location) {
        scope = $rootScope.$new();
        $q = _$q_;
        mockApiService = apiService;
        mockLocation = $location;

        accountGridController = function() {
            return $controller('accountGridController', {
                '$scope': scope,
                'apiService': mockApiService
            });
        };

        getAccounts = $q.defer();
        spyOn(mockApiService, 'getAccounts').and.returnValue(getAccounts.promise);

    }));

    it('invoke controller', function() {
        var controller = accountGridController();
    });

    it('accounts load', function() {
        var controller = accountGridController();
        getAccounts.resolve({data: [{userId: 1}]})
        scope.$apply();

        expect(scope.accounts.length).toBe(1);
        expect(scope.accounts[0].userId).toBe(1);
    });

    it('accounts loading failure displays an error, error description', function() {
        var controller = accountGridController();
        getAccounts.reject({data: { error: {}}})
        scope.$apply();
        expect(controller.loadingError).toBe(true);
    });      

    it('edit account click navigates to /account/{userId}', function() {
        var controller = accountGridController();
        controller.editAccountClick(1);
        scope.$apply();
        expect(mockLocation.path()).toBe("/account/1");
    });  
    
});