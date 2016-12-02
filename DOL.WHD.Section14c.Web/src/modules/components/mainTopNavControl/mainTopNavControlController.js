'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainTopNavControlController', function($scope, $location, stateService, autoSaveService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        this.dashboardClick = function() {
            $location.path("/");
        }

        this.userClick = function() {
            $location.path("/changePassword")
        }

        this.saveClick = function() {
            autoSaveService.save(function () {
                stateService.logOut();
                $location.path("/");
            });
        }
    });
}
