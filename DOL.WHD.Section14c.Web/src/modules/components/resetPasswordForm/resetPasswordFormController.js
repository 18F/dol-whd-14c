'use strict';

module.exports = function(ngModule) {
    ngModule.controller('resetPasswordFormController', function($scope, $location, stateService, apiService, vcRecaptchaService, _env) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        vm.resetErrors = function() {
            vm.forgotPasswordError = false;
            vm.forgotPasswordSuccess = false;
            vm.resetPasswordError = false;
            vm.resetPasswordSuccess = false;
        }

        vm.resetErrors()

        vm.resetPasswordVerificationUrl = $location.absUrl();
        vm.resetPasswordVerificationCode = $location.search().code;
        vm.resetPasswordVerificationUserId = $location.search().userId;
        vm.isResetPasswordVerificationRequest = vm.resetPasswordVerificationCode !== undefined && vm.resetPasswordVerificationUserId !== undefined

        $scope.formVals = {
            'newPass': '',
            'confirmPass': ''
        };

        $scope.onSubmitClick = function() {
            vm.resetErrors();
            apiService.resetPassword($scope.formVals.email, vm.resetPasswordVerificationUrl).then(function (result) {
                var data = result.data;
                vm.forgotPasswordSuccess = true;
                $scope.formVals.email = '';
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                vm.forgotPasswordError = true;
            });
      }

      $scope.onVerifySubmitClick = function() {
            vm.resetErrors();

            apiService.verifyResetPassword(vm.resetPasswordVerificationUserId, $scope.formVals.newPass, $scope.formVals.confirmPass, vm.resetPasswordVerificationCode).then(function (result) {
                var data = result.data;
                vm.resetPasswordSuccess = true;
                $scope.formVals.newPass = '';
                $scope.formVals.confirmPass = '';
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                $scope.resetPasswordErrors = apiService.parseErrors(error.data);
                vm.resetPasswordError = true;
            });
      }


  });
}
