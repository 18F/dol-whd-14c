'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userLoginFormController', function($rootScope, $scope, $location, stateService, apiService, autoSaveService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;
        vm.loginError = false;

        $scope.formVals = {
            'email': '',
            'pass': ''
        };

        $scope.inputType = 'password';

        $scope.onSubmitClick = function() {
            vm.clearError();

            //  Call Token Service
            apiService.userLogin($scope.formVals.email, $scope.formVals.pass).then(function (result) {
                var data = result.data;

                $rootScope.loggedIn = true;

                stateService.access_token = data.access_token;
                // Get User Info
                apiService.userInfo(stateService.access_token).then(function (result) {
                    var data = result.data;
                    stateService.user = data;
                    stateService.ein = data.organizations[0].ein; //TODO: Add EIN selection?

                    // Get Application State for Organization
                    apiService.getApplication(stateService.access_token, stateService.ein).then(function (result) {
                        var data = result.data;
                        stateService.setFormData(JSON.parse(data));
                    }, function (error) {
                        handleError(error);
                    });


                    // start auto-save
                    autoSaveService.start();

                }, function (error) {
                    handleError(error);
                });

                $location.path("/");
            }, function (error) {
                handleError(error);
            });
        }

        var handleError = function(error) {
            console.log(error);

            if (error.status === 400) {
                vm.loginError = true;
            }
            else {
                // catch all error, currently possible to get a 500 if the database server is not reachable
                vm.unknownError = true;
            }

            $location.path("/");
        }

        this.clearError = function() {
            vm.loginError = false;
            vm.unknownError = false;
        }


        $scope.forgotPassword = function() {
            $location.path("/forgotPassword");
        }

        $scope.hideShowPassword = function(){
            if ($scope.inputType === 'password')
                $scope.inputType = 'text';
            else
                $scope.inputType = 'password';
        };

  });
}
