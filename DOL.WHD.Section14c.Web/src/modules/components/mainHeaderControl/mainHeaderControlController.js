'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainHeaderControlController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        let vm = this;
        vm.stateService = stateService;

        $scope.onNavClick = function() {

        }
    });
}
