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
            apiService.userLogin($scope.formVals.email, $scope.formVals.pass).then(function (result) {
                var data = result.data;

                $rootScope.loggedIn = true;

                stateService.access_token = data.access_token;
                stateService.user.email = data.email;

                //TODO: get full user info from the /api/account/userinfo endpoint

                $location.path("/");
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                $location.path("/");
            });
      }
  });
}
