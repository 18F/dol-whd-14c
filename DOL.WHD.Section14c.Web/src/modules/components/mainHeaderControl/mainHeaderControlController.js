'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainHeaderControlController', function($scope, $rootScope, $location, assetService, stateService, apiService, autoSaveService) {
        'ngInject';
        'use strict';

        $scope.appData = stateService.appData;

        var vm = this;
        vm.stateService = stateService;
        vm.assetService = assetService;

        this.userClick = function() {
            $location.path("/");
        }

        this.saveClick = function() {
            autoSaveService.save(function () {
                stateService.logOut();
                $location.path("/");
            });
        }
    });
}
