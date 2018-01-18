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
    vm.loginError= {
      status: false,
      message: ''
    };
    vm.formTitle ='Log in';
    vm.submittButtonName ='Log in';
    vm.twoFAStatus = false;

    $scope.formVals = {
      email: '',
      pass: '',
      code: ''
    };

    $scope.inputType = 'password';

    $scope.onSubmitClick = function() {
      vm.submittingForm = true;
      stateService.user.loginEmail = $scope.formVals.email;

      vm.clearError();
      //  Call Token Service
      authService.userLogin($scope.formVals.email, $scope.formVals.pass, $scope.formVals.code).then(
        function() {
          vm.submittingForm = false;
          if(stateService.user.organizations.length) {
            $location.path("/dashboard");
          }
          else if ($location.path() === '/employerRegistration') {
            $route.reload();
          } else {
            $location.path("/employerRegistration");
          }
        },
        function(error) {
          handleError(error);
          vm.submittingForm = false;
        }
      );
    };

    var handleError = function(error) {
      if (error.data && error.data.error_description === 'Password expired') {
        stateService.user.passwordExpired = true;
        $location.path('/changePassword');
        $scope.$apply();
      }
      vm.loginError = {
        status: true
      }
      
      if (error.status === 400) {
        if(error.data.error === 'locked_out'){
          // update error message
          vm.loginError.message = error.data.error_description
        }
        else{
          if(error.data.error === 'need_code'){
            vm.loginError.message =  error.data.error_description;
            vm.twoFAStatus = true;
            vm.submittButtonName ='Verify';
            vm.formTitle ="Two-factor authentication";
            vm.clearError();
          }
          else{
            vm.loginError.message =  error.data.error_description
          }
        }
      } else {
        // catch all error, currently possible to get a 500 if the database server is not reachable
        vm.unknownError = true;
      }
    };

    this.clearError = function() {
      vm.loginError= {
        status: false,
        message: ''
      };
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
