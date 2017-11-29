'use strict';

module.exports = function(ngModule) {
  ngModule.controller('employerRegistrationController', function(
    $scope,
    stateService,
    $location,
    apiService
  ) {
    'ngInject';
    'use strict';

    $scope.stateService = stateService;

    $scope.navToLanding = function() {
      $location.path('/home');
    };

    $scope.showDetails = false;
    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
      log.info($scope.showDetails);
    }

    $scope.onSubmitClick = function () {
      $scope.formData.isAdmin = $scope.stateService.isAdmin;
      $scope.formData.employer.ein = $scope.formData.ein;
      apiService.setEmployer($scope.stateService.access_token, $scope.formData).then(function(result) {
        console.log(result);
        $location.path('/home');
      }).catch(function(error) {
        console.log(error, "error")
      })
    }
  });
};
