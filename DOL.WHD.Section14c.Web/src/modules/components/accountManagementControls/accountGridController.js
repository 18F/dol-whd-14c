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

    apiService.getAccounts(stateService.access_token).then(
      function(result) {
        var data = result.data;
        $scope.accounts = data;
      },
      function() {
        vm.loadingError = true;
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

      $scope.closeModal();
    }

    $scope.resendConfirmationEmail = function () {
      adminApiService.resendConfirmationEmail(stateService.access_token, $scope.userId);
    }

    $scope.resetPassword = function () {
      adminApiService.resetPassword(stateService.access_token, $scope.userEmail, $scope.password, $scope.confirmPassword);
    }

    $scope.resendCode = function () {
      adminApiService.resendCode(stateService.access_token, $scope.userId);
    }
  });
};
