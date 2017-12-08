'use strict';

module.exports = function(ngModule) {
  ngModule.controller('landingPageController', function(
    $scope,
    stateService,
    apiService,
    $location,
    $route
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

    $scope.navToApplication = function(index) {

      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;
      console.log(index, $scope.organizations[index].applicationId, stateService.applicationId )
      stateService.saveNewApplication().then(
        function(result) {
          console.log('success');
          // start auto-save
          if (stateService.ein) {
            //autoSaveService.start();
          }
        
        },
        function(error) {
          console.log(error);
        }
      );

      //$location.path('/section/assurances');
    };

    $scope.navToEmployerRegistration = function ()  {
      $location.path('/registerEmployer')
    };

  });
};
