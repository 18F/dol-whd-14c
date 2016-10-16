'use strict';

module.exports = function(ngModule) {
    ngModule.controller('formFooterControlsController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;
    });
}
