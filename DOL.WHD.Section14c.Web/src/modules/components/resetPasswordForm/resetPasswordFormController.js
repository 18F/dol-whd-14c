'use strict';

var zxcvbn = require('zxcvbn');

module.exports = function(ngModule) {
  ngModule.controller('resetPasswordFormController', function(
    $scope,
    $location,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;

    vm.resetErrors = function() {
      vm.forgotPasswordError = false;
      vm.forgotPasswordSuccess = false;
      vm.resetPasswordError = false;
      vm.resetPasswordSuccess = false;
      vm.showPasswordHelp = false;
    };

    vm.resetErrors();

    vm.resetPasswordVerificationUrl = $location.absUrl();
    vm.resetPasswordVerificationCode = $location.search().code;
    vm.resetPasswordVerificationUserId = $location.search().userId;
    vm.isResetPasswordVerificationRequest =
      vm.resetPasswordVerificationCode !== undefined &&
      vm.resetPasswordVerificationUserId !== undefined;

    vm.resetPasswordComplexity = function() {
      vm.passwordLength = false;
      vm.passwordUpper = false;
      vm.passwordLower = false;
      vm.passwordSpecial = false;
      vm.passwordNumber = false;
    };
    vm.resetPasswordComplexity();

    $scope.inputType = 'password';
    $scope.$watch('formVals.newPass', function(value) {
      $scope.passwordStrength = zxcvbn(value);
      vm.passwordLength = value.length > 7;
      vm.passwordUpper = value.match(new RegExp('^(?=.*[A-Z])')) ? true : false;
      vm.passwordLower = value.match(new RegExp('^(?=.*[a-z])')) ? true : false;
      vm.passwordSpecial = value.match(new RegExp('^(?=.*[-+_!@#$%^&*.,?])'))
        ? true
        : false;
      vm.passwordNumber = value.match(new RegExp('^(?=.*[0-9])'))
        ? true
        : false;
    });

    $scope.formVals = {
      newPass: '',
      confirmPass: ''
    };

    $scope.onSubmitClick = function() {
      vm.resetErrors();
      apiService
        .resetPassword($scope.formVals.email, vm.resetPasswordVerificationUrl)
        .then(
          function() {
            vm.forgotPasswordSuccess = true;
            $scope.formVals.email = '';
          },
          function(error) {
            console.log(
              error.statusText +
                (error.data && error.data.error
                  ? ': ' +
                    error.data.error +
                    ' - ' +
                    error.data.error_description
                  : '')
            );
            vm.forgotPasswordError = true;
          }
        );
    };

    $scope.hideShowPassword = function() {
      if ($scope.inputType === 'password') $scope.inputType = 'text';
      else $scope.inputType = 'password';
    };

    $scope.onVerifySubmitClick = function() {
      vm.resetErrors();

      apiService
        .verifyResetPassword(
          vm.resetPasswordVerificationUserId,
          $scope.formVals.newPass,
          $scope.formVals.confirmPass,
          vm.resetPasswordVerificationCode
        )
        .then(
          function() {
            vm.resetPasswordSuccess = true;
            $scope.formVals.newPass = '';
            $scope.formVals.confirmPass = '';
          },
          function(error) {
            console.log(
              error.statusText +
                (error.data && error.data.error
                  ? ': ' +
                    error.data.error +
                    ' - ' +
                    error.data.error_description
                  : '')
            );
            $scope.resetPasswordErrors = apiService.parseErrors(error.data);
            vm.resetPasswordError = true;
          }
        );
    };
  });
};
