'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionWioaController', function($scope, stateService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        var vm = this;
  });
}
