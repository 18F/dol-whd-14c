'use strict';

module.exports = function(ngModule) {
  ngModule.controller('employerRegistrationController', function(
    $scope,
    stateService,
    validationService,
    $location,
    apiService
  ) {
    'ngInject';
    'use strict';
    $scope.showAllHelp = false
    $scope.formData = {};
    $scope.formData.employer = {};
    $scope.stateService = stateService;
    $scope.registrationSuccess = false;
    $scope.formIsValid = true;
    $scope.territoriesAndDistricts = ['DC','AS','GU','MP','PR','UM','VI'];
    $scope.resetErrors = function () {
      $scope.einRequired = false;
      $scope.einInvalid = false;
      $scope.hasTradeNameRequired = false;
      $scope.legalNameRequired = false;
      $scope.streetAddressRequired = false;
      $scope.cityRequired = false;
      $scope.zipCodeRequired = false;
      $scope.zipCodeInvalid = false;
      $scope.stateRequired = false
      $scope.certificateNumberRequired = false;
      $scope.certificateNumberInvalid = false
      $scope.countyRequired = false;
    }


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

    $scope.checkRequiredValues = function () {
      if($scope.formData.employer.hasTradeName === undefined) {
        $scope.hasTradeNameRequired = true;
        $scope.formIsValid = false;
      }
      if($scope.formData.employer.hasTradeName && !$scope.formData.employer.certificateNumber) {
        $scope.certificateNumberRequired = true;
        $scope.formIsValid = false;
      }
      if(!$scope.formData.employer.legalName) {
        $scope.legalNameRequired = true;
        $scope.formIsValid = false;
      }
      if(!$scope.formData.employer.ein) {
        $scope.einRequired = true;
        $scope.formIsValid = false;
      } else {
        if(!validationService.validateEIN($scope.formData.employer.ein)) {
          $scope.einInvalid = true;
          $scope.formIsValid = false;
        }
      }
      if(!$scope.formData.employer.physicalAddress) {
        $scope.streetAddressRequired = true;
        $scope.zipCodeRequired = true;
        $scope.stateRequired = true;
        $scope.cityRequired = true;
        $scope.countyRequired = true;
        $scope.formIsValid = false;
      } else {
        if(!$scope.formData.employer.physicalAddress.streetAddress) {
          $scope.streetAddressRequired = true;
          $scope.formIsValid = false;
        }
        if(!$scope.formData.employer.physicalAddress.state) {
          $scope.stateRequired = true;
          $scope.formIsValid = false;
        }
        if(!$scope.formData.employer.physicalAddress.city) {
          $scope.cityRequired = true;
          $scope.formIsValid = false;
        }
        if(!$scope.formData.employer.physicalAddress.county) {
          $scope.countyRequired = true;
          $scope.formIsValid = false;
        }
        if(!$scope.formData.employer.physicalAddress.zipCode) {
          $scope.zipCodeRequired = true;
          $scope.formIsValid = false;
        } else {
          if(!validationService.validateZipCode($scope.formData.employer.physicalAddress.zipCode)) {
            $scope.zipCodeInvalid = true;
            $scope.formIsValid = false;
          }
        }
        if(!$scope.formData.employer.physicalAddress.county) {
          $scope.countyRequired = true;
          $scope.formIsValid = false;
        }
      }
    }




    $scope.onSubmitClick = function () {
      $scope.resetErrors();
      $scope.checkRequiredValues();
      if($scope.formIsValid) {
        $scope.formData.ein = $scope.formData.employer.ein;
        $scope.formData.IsPointOfContact = true;
        apiService.setEmployer($scope.stateService.access_token, $scope.formData).then(function() {
          $scope.registrationSuccess = true;
        }).catch(function(error) {
          if(error.status === 302) {
            $scope.previouslyRegistered = {};
            $scope.previouslyRegistered.status = true;
            $scope.previouslyRegistered.name = error.data;
          } else {
            $scope.registrationSuccess = false;
          }
        });
      }
    }

    $scope.$watch('formData.employer.physicalAddress.state', function () {
      if(!$scope.formData.employer.physicalAddress && !$scope.formData.employer.mailingAddress.state) {
        return;
      }
      if($scope.formData.employer.physicalAddress.state && $scope.territoriesAndDistricts.indexOf($scope.formData.employer.physicalAddress.state) >= 0) {
        $scope.formData.employer.physicalAddress.county = 'N/A';
      }
    });
  });
};
