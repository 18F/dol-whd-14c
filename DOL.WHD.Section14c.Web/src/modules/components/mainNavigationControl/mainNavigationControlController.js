'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainNavigationControlController', function($scope, $location, $route, navService, stateService, apiService) {
        'ngInject';
        'use strict';

        $scope.sections = navService.getSections();

        var vm = this;
        vm.stateService = stateService;
        vm.current = $route.current.params.section_id;

        this.onNavClick = function(event) {
            var id = event.target.dataset.sectionid;
            navService.gotoSection(id);
        }
    });
}
