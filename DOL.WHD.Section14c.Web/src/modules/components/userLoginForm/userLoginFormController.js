'use strict';

module.exports = function(ngModule) {
  ngModule.controller('userLoginFormController', function(
    $rootScope,
    $scope,
    $location,
    $route,
    stateService,
    authService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;
    vm.loginError = false;

    $scope.formVals = {
      email: '',
      pass: ''
    };

    $scope.inputType = 'password';

    $scope.onSubmitClick = function() {
      vm.submittingForm = true;
      stateService.user.loginEmail = $scope.formVals.email;

      vm.clearError();

      //  Call Token Service
      authService.userLogin($scope.formVals.email, $scope.formVals.pass).then(
        function() {
          vm.submittingForm = false;

          if ($location.path() === '/') {
            $route.reload();
          } else {
            $location.path('/');
          }
        },
        function(error) {
          handleError(error);
          vm.submittingForm = false;
        }
      );
    };

    var handleError = function(error) {
      log.info(error);
      if (error.data && error.data.error_description === 'Password expired') {
        stateService.user.passwordExpired = true;
        $location.path('/changePassword');
        $scope.$apply();
      }

      if (error.status === 400) {
        vm.loginError = true;
      } else {
        // catch all error, currently possible to get a 500 if the database server is not reachable
        vm.unknownError = true;
      }
    };

    this.clearError = function() {
      vm.loginError = false;
      vm.unknownError = false;
    };

    $scope.forgotPassword = function() {
      $location.path('/forgotPassword');
    };

    $scope.hideShowPassword = function() {
      if ($scope.inputType === 'password') $scope.inputType = 'text';
      else $scope.inputType = 'password';
    };
  });
};
