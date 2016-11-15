'use strict';

module.exports = function(ngModule) {
    ngModule.controller('adminReviewController', function($scope, stateService) {
        'ngInject';
        'use strict';

        $scope.appData = stateService.appData;

        if (!$scope.appid || !$scope.appData.id !== $scope.appid) {
            //TODO: retrieve app data
        }
    });
}
