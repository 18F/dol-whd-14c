'use strict';

module.exports = function(ngModule) {
  ngModule.controller('systemUseController', function(
    $scope,
    stateService,
    $location,
    apiService
  ) {
    'ngInject';
    'use strict';
    $scope.stateService = stateService;
    $scope.isEmailVerificationRequest = true;
    // redirect to dashboar (home) if user is logged in
    if (stateService.loggedIn) {
      console.log("logged in")
      $location.path('/dashboard');
    }

    $scope.navToLanding = function() {
      $location.path('/login');
    };

    $scope.showDetails = false;

    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
    }


      $scope.emailVerificationUrl = $location.absUrl();
        $scope.emailVerificationCode = $location.search().code;
        $scope.emailVerificationUserId = $location.search().userId;
        //$scope.isEmailVerificationRequest = $scope.emailVerificationCode && $scope.emailVerificationUserId ? true: false;

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
                console.log('here')
                $scope.emailVerificationError = true;
              }
            );
        }
  });
};
