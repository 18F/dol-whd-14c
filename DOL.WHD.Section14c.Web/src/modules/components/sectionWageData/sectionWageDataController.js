'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionWageDataController', function($scope, $location, stateService) {
        'ngInject';
        'use strict';

        var section = this;
        section.stateService = stateService;
        section.activeTab = 1;

        $scope.formData = { };
  });
}
