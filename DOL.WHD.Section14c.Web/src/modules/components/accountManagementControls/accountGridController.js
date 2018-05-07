'use strict';

module.exports = function(ngModule) {
  ngModule.controller('accountGridController', function(
    $scope,
    $location,
    stateService,
    apiService,
    adminApiService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;
    vm.loadingError = false;
    $scope.inputType = 'password';

    $scope.gridOptions = {
      data: [],
      sort: {
         // predicate: 'email',
         // direction: 'asc'
      }
    };  

    //$scope.gridActions = {};

    vm.update = {
      status: '',
      message: ''
    };

    vm.setupdateStatus = function (status, message) {
      vm.update.status = status;
      vm.update.message = message;
    };

    apiService.getAccounts(stateService.access_token).then(
      function(result) {
        var data = result.data;
        $scope.accounts = data;
        $scope.gridOptions.data = data;
      },
      function() {
        vm.loadingError = true;
        $scope.gridOptions.data = [];        
      }
    );

    
    $scope.modalIsVisible = false;
    $scope.showResendEmailModalIsVisible = false;
    $scope.resendCodeModalIsVisible = false;
    $scope.resetPasswordModalIsVisible = false;
    vm.editAccountClick = function(userId) {
      $location.path('/account/' + userId);
    };

    $scope.showModal = function (modalType, account) {
      vm.setupdateStatus('Initialize', '');
      $scope.userEmail = account.email;
      $scope.userId = account.userId;
      $scope[modalType] = true;
      $scope.modalIsVisible = true;
    };

    $scope.closeModal = function () {
      $scope.modalIsVisible = false;
      $scope.resendEmailModalIsVisible = false;
      $scope.resendCodeModalIsVisible = false;
      $scope.resetPasswordModalIsVisible = false;
    };

    $scope.hideShowPassword = function() {
      if ($scope.inputType === 'password') $scope.inputType = 'text';
      else $scope.inputType = 'password';
    };

    $scope.submit = function () {
      if($scope.resendEmailModalIsVisible) {
        $scope.resendConfirmationEmail();
      }

      if($scope.resendCodeModalIsVisible) {
        $scope.resendCode();
      }

      if($scope.resetPasswordModalIsVisible) {
        $scope.resetPassword();
      }
    }

    $scope.resendConfirmationEmail = function () {
      adminApiService.resendConfirmationEmail(stateService.access_token, $scope.userId).then(
        function(result) {
          var statusCode = result.status;
          if(statusCode == 200){
            // Display Success Message
            vm.setupdateStatus('Success', 'Confirmation email has been resend successfully.');
          }
        },
        function() {
          vm.loadingError = true;
          vm.setupdateStatus('Failure', 'Confirmation email resend failed. Please try again.');
        });
    }

    $scope.resetPassword = function () {
      adminApiService.resetPassword(stateService.access_token, $scope.userEmail, $scope.password, $scope.confirmPassword).then(
        function(result) {
          var statusCode = result.status;
          if(statusCode == 200){
            // Display Success Message
            vm.setupdateStatus('Success', 'Password has been reset successfully');
          }
        },
        function() {
          vm.loadingError = true;
          vm.setupdateStatus('Failure', 'Password reset failed. Please try again.');
        });
    }

    $scope.resendCode = function () {
      adminApiService.resendCode(stateService.access_token, $scope.userId).then(
        function(result) {
          if(result.status == 200){
            // Display Success Message
            var data = result.data;
            vm.setupdateStatus('Success', 'Authentication code has been resend successfully. New authentication code: ' + data.code);
          }
        },
        function() {
          vm.loadingError = true;
          vm.setupdateStatus('Failure', 'Authentication code resent failed. Please try again.');
        });
    }
  });
};
