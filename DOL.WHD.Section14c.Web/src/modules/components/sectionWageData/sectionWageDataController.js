'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionWageDataController', function($scope, stateService, responsesService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        // multiple choice responses
        let questionKeys = [ 'PayType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        var vm = this;
        vm.activeTab = 1;
        vm.showLinks = false;
        vm.showAllHelp = false;
  });
}
