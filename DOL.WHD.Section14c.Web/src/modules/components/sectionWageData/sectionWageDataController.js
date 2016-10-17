'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionWageDataController', function($scope, $location, stateService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        var vm = this;
        vm.activeTab = 1;
        vm.showLinks = false;
        vm.showAllHelp = false;
  });
}
