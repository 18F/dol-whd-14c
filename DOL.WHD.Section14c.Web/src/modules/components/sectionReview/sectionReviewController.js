'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionReviewController', function(
    $scope,
    $location,
    apiService,
    stateService,
    navService,
    validationService,
    _constants
  ) {
    'ngInject';
    'use strict';

    stateService.inReview = true;
    $scope.isValid = validationService.validateForm();
    $scope.validation = $scope.isValid
      ? {}
      : validationService.getValidationErrors();
    $scope.navService = navService;
    $scope.hideWageDataSection = false;
    if( stateService.formData.applicationTypeId === _constants.responses.applicationType.initial){
      $scope.hideWageDataSection = true;
    }
    let vm = this;

    this.onSubmit = function() {
      // clear error
      vm.submittingApplication = true;
      vm.submissionError = undefined;

      // submit the application
      if ($scope.isValid) {
        apiService
          .submitApplication(
            stateService.access_token,
            stateService.ein,
            stateService.applicationId,
            stateService.formData
          )
          .then(
            function() {
              vm.submissionSuccess = true;
              vm.submittingApplication = false;
              stateService.resetFormData();
              stateService.applicationId = null;
            },
            function(error) {
              vm.submittingApplication = false;
              vm.submissionError = error;
            }
          );
      }
    };

    this.onHomeClick = function() {
      $location.path('/');
    };
  });
};
