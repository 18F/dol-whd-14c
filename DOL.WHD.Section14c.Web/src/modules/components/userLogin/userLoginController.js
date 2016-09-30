'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userLoginController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        let vm = this;
        vm.stateService = stateService;

        $scope.formVals = {
            'email': '',
            'pass': '',
        };

        $scope.onSubmitClick = function() {
            apiService.userLogin($scope.formVals.email, $scope.formVals.pass).then(function (result) {
                var data = result.data;
                stateService.access_token = data.access_token;
                stateService.username = data.userName;
                $location.path("/");
            }, function (error) {
                alert(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                $location.path("/");
            });
      }
  });
}
