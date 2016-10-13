'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userLoginFormController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        let vm = this;
        vm.stateService = stateService;

        $scope.formVals = {
            'email': '',
            'pass': ''
        };

        $scope.onSubmitClick = function() {
            apiService.userLogin($scope.formVals.email, $scope.formVals.pass).then(function (result) {
                var data = result.data;
                stateService.access_token = data.access_token;
                stateService.email = data.email;
                $location.path("/");
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                $location.path("/");
            });
      }
  });
}
