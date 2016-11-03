'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionReviewController', function($scope, apiService, stateService, navService, validationService) {
        'ngInject';
        'use strict';

        stateService.inReview = true;
        $scope.isValid = validationService.validateForm();
        $scope.validation = $scope.isValid ? {} : validationService.getValidationErrors();
        $scope.navService = navService;

        let vm = this;

        this.onSubmit = function() {
            //TODO: submit the application
        }
    });
}
