'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionReviewController', function(
    $scope,
    $location,
    apiService,
    stateService,
    navService,
    validationService
  ) {
    'ngInject';
    'use strict';

    stateService.inReview = true;
    $scope.isValid = validationService.validateForm();
    $scope.validation = $scope.isValid
      ? {}
      : validationService.getValidationErrors();
    $scope.navService = navService;
    let vm = this;

    this.onSubmit = function() {
      // clear error
      vm.submissionError = undefined;

      // submit the application
      if ($scope.isValid) {
        apiService
          .submitApplication(
            stateService.access_token,
            stateService.ein,
            stateService.formData
          )
          .then(
            function() {
              vm.submissionSuccess = true;
              $scope.isSubmitted = true;
              stateService.resetFormData();
            },
            function(error) {
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
