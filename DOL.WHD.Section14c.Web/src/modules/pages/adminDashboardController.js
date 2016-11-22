'use strict';

module.exports = function(ngModule) {
    ngModule.controller('adminDashboardController', function($scope, $location, stateService) {
        'ngInject';
        'use strict';

        $scope.gridOptions = {
            data: stateService.appList,
            sort: {
                predicate: 'employerName',
                direction: 'asc'
            }
        };

        //$scope.gridActions = {};

        $scope.gotoApplication = function(id) {
            $location.path("/admin/" + id);
        }

        $scope.gotoUsers = function() {
            $location.path("/admin/users");
        }
    });
}
