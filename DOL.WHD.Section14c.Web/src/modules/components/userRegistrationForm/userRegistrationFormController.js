'use strict';

module.exports = function(ngModule) {
    ngModule.controller('userRegistrationFormController', function($scope, $location, stateService, apiService, vcRecaptchaService, _env) {
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

        $scope.inputType = 'password';

        $scope.onSubmitClick = function() {
            apiService.userRegister($scope.formVals.ein, $scope.formVals.email, $scope.formVals.pass, $scope.formVals.confirmPass, $scope.response).then(function (result) {
                $location.path("/");
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                //vcRecaptchaService.reload($scope.widgetId);
                $location.path("/");
            });
        }
        
        $scope.response = null;
        $scope.widgetId = null;
        $scope.model = {
            key: _env.reCaptchaSiteKey
        };
        $scope.setResponse = function (response) {
            console.info('Response available');
            $scope.response = response;
        };
        $scope.setWidgetId = function (widgetId) {
            console.info('Created widget ID: %s', widgetId);
            $scope.widgetId = widgetId;
        };
        $scope.cbExpiration = function() {
            console.info('Captcha expired. Resetting response object');
            vcRecaptchaService.reload($scope.widgetId);
            $scope.response = null;
        };

        $scope.hideShowPassword = function(){
            if ($scope.inputType === 'password')
                $scope.inputType = 'text';
            else
                $scope.inputType = 'password';
        };

  });
}
