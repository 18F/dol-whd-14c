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
    if(!$scope.formData) {
      $scope.formData = {
        employer: {
          physicalAddress: {}
        }
      }
    }
    $scope.previouslyRegistered = {
      status: false,
      name: ""
    }

    $scope.registrationSuccess = false;
    $scope.formIsValid = true;
    apiService.userInfo(stateService.access_token).then(function(result){
      if(result.data.organizations.length>0) {
        $location.path("/dashboard");
      }
    });
    $scope.validationProperties = {
      streetAddressRequired: false,
      cityRequired: false,
      zipCodeRequired: false,
      zipCodeInvalid: false,
      stateRequired: false,
      countyRequired: false,
      einRequired: false,
      einInvalid: false,
      hasTradeNameRequired: false,
      legalNameRequired: false,
      certificateNumberRequired: false,
      certificateNumberInvalid: false
    }

    $scope.validateForm = function () {
      for(var property in $scope.validationProperties) {
        if($scope.validationProperties[property]) {
          $scope.formIsValid = false;
        }
      }
    }

    $scope.territoriesAndDistricts = ['DC','AS','GU','MP','PR','UM','VI'];

    $scope.resetErrors = function () {
      for(var property in $scope.validationProperties) {
        $scope.validationProperties[property] = false;
      }
    }

    $scope.toggleAllHelpText = function () {
      $scope.showAllHelp = !$scope.showAllHelp;
    };

    $scope.validateAddress = function () {
      if(!$scope.formData.employer.physicalAddress.streetAddress) {
        $scope.validationProperties.streetAddressRequired = true;
      }
      if(!$scope.formData.employer.physicalAddress.state) {
        $scope.validationProperties.stateRequired = true;
      }
      if(!$scope.formData.employer.physicalAddress.city) {
        $scope.validationProperties.cityRequired = true;
      }
      if(!$scope.formData.employer.physicalAddress.county) {
        $scope.validationProperties.countyRequired = true;
      }
      if(!$scope.formData.employer.physicalAddress.zipCode) {
        $scope.validationProperties.zipCodeRequired = true;
      } else {
        if(!validationService.validateZipCode($scope.formData.employer.physicalAddress.zipCode)) {
          $scope.zipCodeInvalid = true;
        }
      }
      if(!$scope.formData.employer.physicalAddress.county) {
        $scope.validationProperties.countyRequired = true;
      }
    }

    $scope.validateEmployer = function () {
      if($scope.formData.employer.hasTradeName === undefined) {
        $scope.validationProperties.hasTradeNameRequired = true;
      }
      if($scope.formData.employer.hasTradeName && !$scope.formData.employer.certificateNumber) {
        $scope.validationProperties.certificateNumberRequired = true;
      } else {
        if(!validationService.validateCertificateNumber($scope.formData.employer.certificateNumber)) {
          $scope.validationProperties.certificateNumberInvalid = true;
        }
      }
      if(!$scope.formData.employer.legalName) {
        $scope.validationProperties.legalNameRequired = true;
      }
      if(!$scope.formData.employer.ein) {
        $scope.validationProperties.einRequired = true;
      } else {
        if(!validationService.validateEIN($scope.formData.employer.ein)) {
          $scope.validationProperties.einInvalid = true;
        }
      }
    }

    $scope.getValidationErrors = function () {
      $scope.validateEmployer();
      $scope.validateAddress();
    }




    $scope.onSubmitClick = function () {
      $scope.resetErrors();
      $scope.getValidationErrors();
      $scope.validateForm();
      if($scope.formIsValid) {
        $scope.formData.ein = $scope.formData.employer.ein;
        $scope.formData.IsPointOfContact = true;
        apiService.setEmployer(stateService.access_token, $scope.formData).then(function() {
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
      if(!$scope.formData.employer.physicalAddress && !$scope.formData.employer.physicalAddress.state) {
        return;
      }
      if($scope.formData.employer.physicalAddress.state && $scope.territoriesAndDistricts.indexOf($scope.formData.employer.physicalAddress.state) >= 0) {
        $scope.formData.employer.physicalAddress.county = 'N/A';
      }
    });
  });
};
