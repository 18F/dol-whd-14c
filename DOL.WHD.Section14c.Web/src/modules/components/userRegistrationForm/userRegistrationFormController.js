'use strict';

import some from 'lodash/some'
var zxcvbn = require('zxcvbn');

module.exports = function(ngModule) {
    ngModule.controller('userRegistrationFormController', function($window, $scope, $location, stateService, apiService, vcRecaptchaService, _env) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        vm.restForm = function() {
            $scope.formVals = {
                'ein': '',
                'email': '',
                'pass': '',
                'confirmPass': ''
            };
        }
        vm.restForm();

        vm.resetErrors = function() {
            vm.showEinHelp = false;
            vm.einError = false;
            vm.einRequired = false;
            vm.emailAddressError = false;
            vm.emailAddressRequired = false;
            vm.reCaptchaError = false;
            vm.showPasswordHelp = false;
            vm.passwordRequired = false;
            vm.invalidEin = false;
            vm.passwordsDontMatch = false;
            vm.passwordComplexity = false;
            vm.accountCreated = false;
            vm.emailVerified = false;
            vm.emailVerificationError = false;
        }
        vm.resetErrors();

        vm.resetPasswordComplexity = function() {
            vm.passwordLength = false;
            vm.passwordUpper = false;
            vm.passwordLower = false;
            vm.passwordSpecial = false;
            vm.passwordNumber = false;
        }
        vm.resetPasswordComplexity();

        vm.toggleEinHelp = function() {
            vm.showEinHelp = !vm.showEinHelp;
        }

        $scope.$watch('formVals.pass', function (value) {
            $scope.passwordStrength = zxcvbn(value);
            vm.passwordLength = value.length > 7;
            vm.passwordUpper = value.match(new RegExp("^(?=.*[A-Z])")) ? true : false;
            vm.passwordLower = value.match(new RegExp("^(?=.*[a-z])"))? true : false;
            vm.passwordSpecial = value.match(new RegExp("^(?=.*[-+_!@#$%^&*.,?])")) ? true : false;
            vm.passwordNumber = value.match(new RegExp("^(?=.*[0-9])")) ? true : false;
        });

        $scope.inputType = 'password';
        vm.emailVerificationUrl = $location.absUrl();
        vm.emailVerificationCode = $location.search().code;
        vm.emailVerificationUserId = $location.search().userId;
        vm.isEmailVerificationRequest = vm.emailVerificationCode !== undefined && vm.emailVerificationCode !== undefined

        if(vm.isEmailVerificationRequest){
            $location.search('code', null);
            $location.search('userId', null);
            vm.resetErrors();
            apiService.emailVerification(vm.emailVerificationUserId, vm.emailVerificationCode, $scope.verifyResponse).then(function (result) {
                vm.emailVerified = true;
            }, function (error) {
                vm.emailVerificationError = true;
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
            });
        }

        $scope.onSubmitClick = function() {
            vm.resetErrors();
            vm.registerdEmail = ''
            apiService.userRegister($scope.formVals.ein, $scope.formVals.email, $scope.formVals.pass, $scope.formVals.confirmPass, $scope.regResponse, vm.emailVerificationUrl).then(function (result) {
                $scope.resetRegCaptcha();
                vm.registerdEmail = $scope.formVals.email
                vm.restForm();
                vm.accountCreated = true;
                $window.scrollTo(0, 0);
            }, function (error) {
                if(error && error.data){
                    $scope.registerErrors = apiService.parseErrors(error.data);
                    if($scope.registerErrors.indexOf("EIN is already registered") > -1){
                        vm.einError = true;
                    }
                    if($scope.registerErrors.indexOf("Unable to validate reCaptcha Response") > -1){
                        vm.reCaptchaError = true;
                    }
                    if(some($scope.registerErrors, function(error) { return error.indexOf("is already taken") > -1;})) {
                        vm.emailAddressError = true;
                    }   
                    if($scope.registerErrors.indexOf("The Email field is required.") > -1){
                        vm.emailAddressRequired = true;
                    }
                    if($scope.registerErrors.indexOf("The Password field is required.") > -1){
                        vm.passwordRequired = true;
                    }
                    if($scope.registerErrors.indexOf("The EIN field is required.") > -1){
                        vm.einRequired = true;
                    }
                    if(some($scope.registerErrors, function(error) { return error.indexOf("The field EIN must match") > -1;})) {
                        vm.invalidEin = true;
                    }   
                    if($scope.registerErrors.indexOf("The password and confirmation password do not match.") > -1){
                        vm.passwordsDontMatch = true;
                    }
                    if($scope.registerErrors.indexOf("Password does not meet complexity requirements.") > -1){
                        vm.passwordComplexity = true;
                    }
                }
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                $scope.resetRegCaptcha();
                $location.path("/");
            });
        }
        
        $scope.regResponse = null;
        $scope.regWidgetId = null;
        $scope.model = {
            key: _env.reCaptchaSiteKey
        };
        $scope.setRegResponse = function (response) {
            console.info('Response available');
            $scope.regResponse = response;
        };
        $scope.createRegWidget = function (widgetId) {
            console.info('Created widget ID: %s', widgetId);
            $scope.regWidgetId  = widgetId;
        };
        $scope.resetRegCaptcha = function() {
            console.info('Captcha expired/reset. Resetting response object');
            vcRecaptchaService.reload($scope.regWidgetId);
            $scope.regResponse = null;
        };

        $scope.hideShowPassword = function(){
            if ($scope.inputType === 'password')
                $scope.inputType = 'text';
            else
                $scope.inputType = 'password';
        };
  });
}
