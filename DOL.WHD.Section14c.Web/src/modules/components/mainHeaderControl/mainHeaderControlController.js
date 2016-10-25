'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainHeaderControlController', function($scope, $rootScope, $location, stateService, apiService, autoSaveService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        // Manually passing loadImage function through since scope is redefined
        // in the directive
        $scope.loadImage = $rootScope.loadImage;

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
