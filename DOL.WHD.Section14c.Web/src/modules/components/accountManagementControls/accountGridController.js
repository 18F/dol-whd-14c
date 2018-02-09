'use strict';

module.exports = function(ngModule) {
  ngModule.controller('accountGridController', function(
    $scope,
    $location,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;
    vm.loadingError = false;

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

    $scope.showModal = function () {
      $scope.modalIsVisible = true;
    };

    $scope.closeModal = function () {
      $scope.modalIsVisible = false;
    };

    $scope.toggleModal = function(modalType) {
      console.log(modalType)
      $scope.modalIsVisible = !$scope.modalIsVisible;
      $scope[modalType] = !$scope[modalType];
    }
  });
};
