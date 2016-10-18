'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userLoginFormController', function($rootScope, $scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        $scope.formVals = {
            'email': '',
            'pass': ''
        };

        $scope.onSubmitClick = function() {
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
            console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
            $location.path("/");
      }
  });
}



