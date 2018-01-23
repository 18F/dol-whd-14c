'use strict';

module.exports = function(ngModule) {
  ngModule.controller('userLoginPageController', function($scope, $location, apiService, $route) {
    'ngInject';
    'use strict';
     $scope.refresh = function() {
       $location.search('timeout', null);
      $route.reload();
    }
    $scope.isEmailVerificationRequest = false
    $scope.emailVerificationUrl = $location.absUrl();
    $scope.emailVerificationCode = $location.search().code;
    $scope.sessionTimeout = $location.search().timeout;
    $scope.emailVerificationUserId = $location.search().userId;
    $scope.isEmailVerificationRequest = $scope.emailVerificationCode && $scope.emailVerificationUserId ? true: false;
    if ($scope.isEmailVerificationRequest) {
      $location.search('code', null);
      $location.search('userId', null);
      apiService
        .emailVerification(
          $scope.emailVerificationUserId,
          $scope.emailVerificationCode
        )
        .then(
          function() {
            $scope.emailVerified = true;
          },
          function() {
            $scope.emailVerificationError = true;
          }
        );
    }
  });
};
