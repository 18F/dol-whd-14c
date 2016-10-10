'use strict';

module.exports = function(ngModule) {
    ngModule.controller('changePasswordFormController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        let vm = this;
        vm.stateService = stateService;

        $scope.formVals = {
            'currentPass': '',
            'newPass': '',
            'confirmPass': ''
        };

        $scope.onSubmitClick = function() {
            apiService.changePassword(stateService.user.email, $scope.formVals.currentPass, $scope.formVals.newPass, $scope.formVals.confirmPass ).then(function (result) {
                var data = result.data;

                //TODO: provide user with confirmation

                $location.path("/");
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));

                //TODO: inform user of error

                $location.path("/");
            });
      }
  });
}
