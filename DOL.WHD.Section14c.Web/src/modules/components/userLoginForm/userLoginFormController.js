'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userLoginFormController', function($rootScope, $scope, $location, stateService, apiService, autoSaveService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        $scope.formVals = {
            'email': '',
            'pass': ''
        };

        $scope.inputType = 'password';

        $scope.onSubmitClick = function() {
            stateService.user.loginEmail = $scope.formVals.email
            
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
            //TODO: Integrate error handling into form
            if(error.data.error_description === 'Password expired'){
                stateService.user.passwordExpired = true;
                $location.path("/changePassword");
                $scope.$apply()
            }
            console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
            $location.path("/");
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



