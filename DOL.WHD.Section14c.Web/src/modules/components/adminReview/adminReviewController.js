'use strict';

module.exports = function(ngModule) {
    ngModule.controller('adminReviewController', function($scope, stateService) {
        'ngInject';
        'use strict';

        $scope.appData = stateService.appData;

        if ($scope.appid && $scope.appid !== $scope.appData.id) {
            stateService.loadApplicationData($scope.appid);
        }
    });
}
