'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainNavigationControlController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        let vm = this;
        vm.stateService = stateService;

        $scope.onNavClick = function() {

        }
    });
}
