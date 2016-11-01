'use strict';

module.exports = function(ngModule) {
    ngModule.controller('resetPasswordFormController', function($scope, $location, stateService, apiService, vcRecaptchaService, _env) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        vm.resetPasswordVerificationUrl = $location.absUrl();
        vm.resetPasswordVerificationCode = $location.search().code;
        vm.resetPasswordVerificationUserId = $location.search().userId;
        vm.isResetPasswordVerificationRequest = vm.resetPasswordVerificationCode !== undefined && vm.resetPasswordVerificationUserId !== undefined

        $scope.formVals = {
            'newPass': '',
            'confirmPass': ''
        };


        $scope.onSubmitClick = function() {

            apiService.resetPassword($scope.formVals.email, vm.resetPasswordVerificationUrl).then(function (result) {
                var data = result.data;

                //TODO: provide user with confirmation

                $location.path("/");
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));

                //TODO: inform user of error

                $location.path("/");
            });
      }

      $scope.onVerifySubmitClick = function() {

            apiService.verifyResetPassword(vm.resetPasswordVerificationUserId, $scope.formVals.newPass, $scope.formVals.confirmPass, vm.resetPasswordVerificationCode).then(function (result) {
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
