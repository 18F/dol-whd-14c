describe('accountFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, $location, _$q_, apiService) {
        scope = $rootScope.$new();
        $q = _$q_;
        mockApiService = apiService;
        mockRouteParams = {};
        mockLocation = $location;

        getAccount = $q.defer();
        spyOn(mockApiService, 'getAccount').and.returnValue(getAccount.promise);
    
        getRoles = $q.defer();
        spyOn(mockApiService, 'getRoles').and.returnValue(getRoles.promise);

        modifyAccount = $q.defer();
        spyOn(mockApiService, 'modifyAccount').and.returnValue(modifyAccount.promise);

        createAccount = $q.defer();
        spyOn(mockApiService, 'createAccount').and.returnValue(createAccount.promise);

        accountFormController = function() {
            return $controller('accountFormController', {
                '$scope': scope,
                '$routeParams': mockRouteParams,
                '$location.': mockLocation
            });
        };
    }));

    it('when editing user forVals get set', function() {
        var controller = accountFormController();
        getAccount.resolve({ data: { userId: 1 } });
        scope.$apply();

        expect(scope.formVals.userId).toBe(1);
    });    

    it('when editing user if the getAccount service has an error show loading error', function() {
        var controller = accountFormController();
        getAccount.reject({ data: { error: 'error'} });
        scope.$apply();

        getAccount.reject({ data: { error: ''} });
        scope.$apply();

        expect(controller.loadingError).toBe(true);
    });         

    it('default formVals if creating a new account', function() {
        mockRouteParams = { userId: 'create'};
        var controller = accountFormController();
        scope.$apply();

        expect(scope.formVals).not.toBe(undefined);
        expect(scope.formVals.roles.length).toBe(0);
    });  

    it('load roles', function() {
        var controller = accountFormController();
        getRoles.resolve({ data: {} });
        scope.$apply();

        expect(scope.roles).not.toBe(undefined); 
    });      

    it('error loading roles', function() {
        var controller = accountFormController();
        getRoles.reject({ data: { error: ''} });
        scope.$apply();

        expect(scope.roles).toBe(undefined);
        expect(controller.loadingError).toBe(true);
    });       

    it('error loading roles, log error details', function() {
        var controller = accountFormController();
        getRoles.reject({ data: { error: 'error'} });
        scope.$apply();

        expect(scope.roles).toBe(undefined);
        expect(controller.loadingError).toBe(true);
    });   

    it('toggle role selection on', function() {
        var controller = accountFormController();
        scope.roles = { id: 1 };
        scope.formVals = { roles : []};
        controller.toggleRole({ id: 1 });
        scope.$apply();

        expect(scope.formVals.roles.length).toBe(1);
    });   

    it('toggle role selection off', function() {
        var controller = accountFormController();
        scope.roles = { id: 1 };
        scope.formVals = { roles : [{ id: 1 }]};
        controller.toggleRole({ id: 1 });
        scope.$apply();

        expect(scope.formVals.roles.length).toBe(0);
    });   

    it('role does not exist', function() {
        var controller = accountFormController();
        scope.formVals = { roles : [{ id: 1 }]};
        var exists = controller.roleExists(2);
        scope.$apply();
        expect(exists).toBe(-1);
    });   

    it('roleExist function does not exist', function() {
        var controller = accountFormController();
        scope.formVals = { roles : []};
        var exists = controller.roleExists(1);
        scope.$apply();
        expect(exists).toBe(-1);
    });

    it('cancel click navigates to landing page', function() {
        var controller = accountFormController();
        controller.cancelClick();
        scope.$apply();
        expect(mockLocation.path()).toBe("/");
    });  

    it('submit edit account success redirects back to landing page', function() {
        var controller = accountFormController();
        controller.submitForm();
        modifyAccount.resolve({data: {}})
        scope.$apply();
        modifyAccount.resolve({data: {}})
        expect(mockLocation.path()).toBe("/");
    });      
    it('submit edit account error displays error', function() {
        var controller = accountFormController();
        controller.submitForm();
        modifyAccount.reject({data: {}})
        scope.$apply();

        expect(controller.savingError).toBe(true);
    }); 

    it('submit edit account error displays erro, log description', function() {
        var controller = accountFormController();
        controller.submitForm();
        modifyAccount.reject({data: { error: {}}})
        scope.$apply();

        expect(controller.savingError).toBe(true);
    });    

    it('submit create account success redirects back to landing page', function() {
        var controller = accountFormController();
        controller.isEditAccount = false;
        controller.submitForm();
        createAccount.resolve({data: {}})
        scope.$apply();
        createAccount.resolve({data: {}})
        expect(mockLocation.path()).toBe("/");
    });    

    it('submit create account error displays error', function() {
        var controller = accountFormController();
        controller.isEditAccount = false;
        controller.submitForm();
        createAccount.reject({data: {}})
        scope.$apply();

        expect(controller.savingError).toBe(true);
    }); 

    it('submit create account error displays erro, log description', function() {
        var controller = accountFormController();
        controller.isEditAccount = false;
        controller.submitForm();
        createAccount.reject({data: { error: {}}})
        scope.$apply();

        expect(controller.savingError).toBe(true);
    });       
           

});