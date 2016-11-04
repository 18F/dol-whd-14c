describe('userRegistrationFormController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, _$q_, apiService) {
        $q = _$q_;
        scope = $rootScope.$new();
        mockapiService = apiService;

        userRegistrationFormController = function() {
            return $controller('userRegistrationFormController', {
                '$scope': scope,
                'apiService': mockapiService
            });
        };

        userRegister = $q.defer();
        spyOn(mockapiService, 'userRegister').and.returnValue(userRegister.promise);

    }));

    it('invoke controller', function() {
        var controller = userRegistrationFormController();
    });

    it('userRegistrationFormController has hideShowPassword', function() {
        var controller = userRegistrationFormController();
        scope.hideShowPassword();
    });    
    it('userRegistrationFormController has setRegResponse', function() {
        var controller = userRegistrationFormController();
        scope.setRegResponse();
    });   
    it('userRegistrationFormController has createRegWidget', function() {
        var controller = userRegistrationFormController();
        scope.createRegWidget();
    });    
    it('userRegistrationFormController has hideShowPassword', function() {
        var controller = userRegistrationFormController();
        scope.hideShowPassword();
    });                   

    it('userRegistrationFormController has hideShowPassword', function() {
        var controller = userRegistrationFormController();
        scope.onSubmitClick();
        scope.$apply();
    });    


});