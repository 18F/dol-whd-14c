'use strict';

module.exports = function(ngModule) {
  ngModule.controller('landingPageController', function(
    $scope,
    stateService,
    autoSaveService,
    apiService,
    $location
  ) {
    'ngInject';
    'use strict';

    apiService.userInfo(stateService.access_token).then(function(result) {
      $scope.organizations = result.data.organizations;
      console.log($scope.organizations)
    });


    $scope.changePassword = function() {
      $location.path('/changePassword');
    };

    $scope.onApplicationClick = function(index) {

      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;

      if($scope.organizations[index].applicationStatus.name === "New") {
         stateService.saveNewApplication().then(
          function() {
            // start auto-save
            if (stateService.ein) {
              autoSaveService.start();
              $location.path('/section/assurances');
            }

          },
          function() {
          }
        );
       } else {
        stateService.loadSavedApplication().then(
          function() {
            // start auto-save
            if (stateService.ein) {
              autoSaveService.start();
              $location.path('/section/assurances');
            }

          },
          function() {
          }
        );
       }

      $location.path('/section/assurances');
    };

    $scope.navToEmployerRegistration = function ()  {
      $location.path('/employerRegistration');
    };

  });
};
