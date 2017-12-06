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
    $scope.stateService = stateService;
    $scope.organizations = $scope.stateService.user.organizations
    console.log($scope.organizations)
    $scope.changePassword = function() {
      $location.path('/changePassword');
    };

    $scope.navToApplication = function(index) {
      stateService.employerId = $scope.organizations[index].employer.id;
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
