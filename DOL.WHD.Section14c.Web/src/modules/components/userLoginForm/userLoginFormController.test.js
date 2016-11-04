describe('userLoginFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, _$q_, apiService) {
        scope = $rootScope.$new();
        $q = _$q_;
        mockapiService = apiService;
        userLoginFormController = function() {
            return $controller('userLoginFormController', {
                '$scope': scope, 
                '$route': route,
                'apiService': mockapiService
            });
        };

        userLogin = $q.defer();
        spyOn(mockapiService, 'userLogin').and.returnValue(userLogin.promise);

        userInfo = $q.defer();
        spyOn(mockapiService, 'userInfo').and.returnValue(userInfo.promise);

    }));

    it('invoke controller', function() {
        var controller = userLoginFormController();
    });

    it('userLoginFormController has clearError', function() {
        var controller = userLoginFormController();
        controller.clearError();
    });

    it('userLoginFormController has forgotPassword', function() {
        var controller = userLoginFormController();
        scope.forgotPassword();
    });

    it('userLoginFormController has hideShowPassword', function() {
        var controller = userLoginFormController();
        scope.hideShowPassword();
    });

    it('userLoginFormController has onSubmitClick', function() {
        var controller = userLoginFormController();
        scope.onSubmitClick();
    });

    it('on login fails', function() {
        var controller = userLoginFormController();

        scope.onSubmitClick();
        userLogin.reject({ data: {} });
        scope.$apply();
    });  

    it('on login fails with pasword expired', function() {
        var controller = userLoginFormController();

        scope.onSubmitClick();
        userLogin.reject({ data: { error_description: 'Password expired'} });
    });  

    it('on login fails with 400', function() {
        var controller = userLoginFormController();

        scope.onSubmitClick();
        userLogin.reject({ status: 400 });
        scope.$apply();
    });  

    it('on user info fails', function() {
        var controller = userLoginFormController();

        scope.onSubmitClick();
        userLogin.resolve({ data: {} });
        userInfo.reject({ data: {} });
        scope.$apply(); 
    });        

    it('on user info fails', function() {
        var controller = userLoginFormController();

        scope.onSubmitClick();
        userLogin.resolve({ data: {} });
        userInfo.reject({ data: {} });
        scope.$apply();
    });         

});