'use strict';

module.exports = function(ngModule) {
    ngModule.controller('appReviewPageController', function($scope, apiService, stateService, validationService) {
        'ngInject';
        'use strict';

        $scope.validation = validationService.validateForm() ? undefined : validationService.getValidationErrors();
    });
}
