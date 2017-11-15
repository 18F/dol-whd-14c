'use strict';

module.exports = function(ngModule) {
  ngModule.controller('changePasswordFormController', function(
    $scope,
    $location,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    $scope.stateService = stateService;

    vm.resetErrors = function() {
      vm.changePasswordError = false;
      vm.changePasswordSuccess = false;
    };

    vm.resetErrors();

    $scope.formVals = {
      currentPass: '',
      newPass: '',
      confirmPass: ''
    };

    $scope.onSubmitClick = function() {
      vm.resetErrors();
      apiService
        .changePassword(
          stateService.access_token,
          $scope.formVals.email,
          $scope.formVals.currentPass,
          $scope.formVals.newPass,
          $scope.formVals.confirmPass
        )
        .then(
          function() {
            stateService.user.loginEmail = '';
            $scope.formVals.currentPass = '';
            $scope.formVals.newPass = '';
            $scope.formVals.confirmPass = '';
            vm.changePasswordSuccess = true;
          },
          function(error) {
            $scope.changePasswordErrors = apiService.parseErrors(error.data);
            vm.changePasswordError = true;
          }
        );
    };
  });
};
