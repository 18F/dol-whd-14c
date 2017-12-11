'use strict';

module.exports = function(ngModule) {
  ngModule.controller('landingPageController', function(
    $scope,
    stateService,
    autoSaveService,
    apiService,
    $location,
    $route
  ) {
    'ngInject';
    'use strict';

    apiService.userInfo(stateService.access_token).then(function(result) {
      $scope.organizations = result.data.organizations;
    });
    

    $scope.changePassword = function() {
      $location.path('/changePassword');
    };

    $scope.startNewApplication = function(index) {

      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;


      stateService.saveNewApplication().then(
        function(result) {
          console.log('success');
          // start auto-save
          if (stateService.ein) {
            autoSaveService.start();
            $location.path('/section/assurances');
          }
        
        },
        function(error) {
          console.log(error);
        }
      );

      //$location.path('/section/assurances');
    };

    $scope.continueApplication = function(index) {

      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;

      stateService.loadSavedApplication().then(
        function(result) {
          console.log('success');
          // start auto-save
          if (stateService.ein) {
            autoSaveService.start();
            $location.path('/section/assurances');
          }
        
        },
        function(error) {
          console.log(error);
        }
      );

      //$location.path('/section/assurances');
    };

    $scope.navToEmployerRegistration = function ()  {
      $location.path('/registerEmployer');
    };

  });
};
