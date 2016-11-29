'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainHeaderControlController', function($scope, $rootScope, $location, assetService, stateService, apiService, autoSaveService) {
        'ngInject';
        'use strict';

        $scope.appData = stateService.appData;

        var vm = this;
        vm.stateService = stateService;
        vm.assetService = assetService;
    });
}
