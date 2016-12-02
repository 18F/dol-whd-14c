'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionAdminSummaryController', function($scope, stateService, statusesService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;

        statusesService.getStatuses().then((data) => {
            vm.statuses = data;
        });

        vm.updateStatus = () => {
            apiService.changeApplicationStatus(stateService.access_token, $scope.appid, $scope.appData.statusId);
        };
    });
};