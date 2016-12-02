'use strict';

module.exports = function(ngModule) {
    ngModule.controller('adminReviewController', function($scope, stateService, _constants) {
        'ngInject';
        'use strict';

        $scope.appData = stateService.appData;
        $scope.constants = _constants;

        if ($scope.appid && $scope.appid !== $scope.appData.id) {
            stateService.loadApplicationData($scope.appid);
        }
    });
}
