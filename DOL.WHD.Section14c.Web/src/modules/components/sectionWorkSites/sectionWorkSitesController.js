'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionWorkSitesController', function($scope, stateService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        var vm = this;
  });
}
