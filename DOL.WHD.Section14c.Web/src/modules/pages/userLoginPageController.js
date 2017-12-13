'use strict';

module.exports = function(ngModule) {
  ngModule.controller('userLoginPageController', function($scope, $location, apiService) {
    'ngInject';
    'use strict';

    var vm = this;

	vm.emailVerificationUrl = $location.absUrl();
    vm.emailVerificationCode = $location.search().code;
    vm.emailVerificationUserId = $location.search().userId;
    vm.isEmailVerificationRequest =
      vm.emailVerificationCode !== undefined &&
      vm.emailVerificationCode !== undefined;

    if (vm.isEmailVerificationRequest) {
      $location.search('code', null);
      $location.search('userId', null);
      apiService
        .emailVerification(
          vm.emailVerificationUserId,
          vm.emailVerificationCode,
          $scope.verifyResponse
        )
        .then(
          function() {
            vm.emailVerified = true;
          },
          function() {
            vm.emailVerificationError = true;
          }
        );
    }
  });
};


