'use strict';

module.exports = function(ngModule) {
  ngModule.controller('employerRegistrationController', function(
    $scope,
    stateService,
    $location,
    autoSaveService,
    apiService
  ) {
    'ngInject';
    'use strict';

    $scope.showAllHelp = false
    $scope.stateService = stateService;
    $scope.registrationSuccess = false;

    $scope.navToLanding = function() {
      $location.path('/dashboard');
    };

    $scope.showDetails = false;
    $scope.toggleDetails = function ()  {
      $scope.showDetails = !$scope.showDetails;
    };

    $scope.toggleAllHelpText = function () {
      $scope.showAllHelp = !$scope.showAllHelp;
    };

    $scope.onSubmitClick = function () {
      if($scope.formData){
        $scope.formData.IsPointOfContact = true;
        $scope.formData.employer.ein = $scope.formData.ein;
        apiService.setEmployer($scope.stateService.access_token, $scope.formData).then(function() {
          $scope.registrationSuccess = true;

          apiService.userInfo(stateService.access_token).then(function(result) {

            $scope.application = result.data.organizations.filter(function(element) {
              return element.applicationStatus.name === "New"
            });

            stateService.employerId = $scope.application.employer.ein;
            stateService.applicationId = $scope.application.applicationId;
            stateService.ein = $scope.application.ein;
            stateService.employerName = $scope.application.employer.legalName;
          });




        }).catch(function(error) {
          if(error.status === 302) {
            $scope.previouslyRegistered = {};
            $scope.previouslyRegistered.status = true;
            $scope.previouslyRegistered.name = error.data;
          }
        });
      }
    }

    $scope.navToDashboard = function ()  {
      $location.path('/dashboard');
    }
  });
};
