'use strict';

module.exports = function(ngModule) {
    ngModule.controller('mainNavigationControlController', function($scope, $location, $route, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        // set stateService.currentSection based on route for consistency
        stateService.currentSection = $route.current.params.section_id;

        this.onNavClick = function(event) {
            console.log("HERE");
            var id = event.target.dataset.sectionid;
            $location.path("/section/" + id);
        }
    });
}
