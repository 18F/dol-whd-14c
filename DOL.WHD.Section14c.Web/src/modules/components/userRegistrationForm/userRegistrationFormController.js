'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userRegistrationFormController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        $scope.formVals = {
            'ein': '',
            'email': '',
            'pass': '',
            'confirmPass': ''
        };

        $scope.onSubmitClick = function() {
            apiService.userRegister($scope.formVals.ein, $scope.formVals.email, $scope.formVals.pass, $scope.formVals.confirmPass).then(function (result) {
                $location.path("/");
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                $location.path("/");
            });
      }
  });
}
