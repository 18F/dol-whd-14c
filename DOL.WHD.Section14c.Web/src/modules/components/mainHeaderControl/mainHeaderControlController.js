'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainHeaderControlController', function($scope, $rootScope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        // Manually passing loadImage function through since scope is redefined
        // in the directive
        $scope.loadImage = $rootScope.loadImage;

        this.userClick = function() {
            //TODO
        }

        this.saveClick = function() {
            //TODO
        }
    });
}
