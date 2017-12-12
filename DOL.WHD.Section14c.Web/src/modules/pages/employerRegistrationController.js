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
    $scope.showAllHelp = false
    $scope.stateService = stateService;

    $scope.navToLanding = function() {
      $location.path('/home');
    };

    $scope.showDetails = false;
    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
      log.info($scope.showDetails);
    };

    $scope.toggleAllHelpText = function () {
      $scope.showAllHelp = !$scope.showAllHelp;
    };

    $scope.onSubmitClick = function () {
      $scope.formData.isAdmin = true;
      $scope.formData.employer.ein = $scope.formData.ein;
      apiService.setEmployer($scope.stateService.access_token, $scope.formData).then(function(result) {
          $scope.registrationSuccess = true;
      }).catch(function(error) {
        console.log(error);
         if(error.status === 302) {
            $scope.previouslyRegistered = {};
            $scope.previouslyRegistered.status = true;
            $scope.previouslyRegistered.name = error.data;
          }
      });
    }
  });
};
