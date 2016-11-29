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
            if (stateService.user && stateService.user.userId) {
                $location.path("/account/" + stateService.user.userId);
            }
            else {
                $location.path("/");
            }
        }

        this.saveClick = function() {
            autoSaveService.save(function () {
                stateService.logOut();
                $location.path("/");
            });
        }
    });
}
