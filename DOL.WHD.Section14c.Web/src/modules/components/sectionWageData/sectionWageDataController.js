'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionWageDataController', function($scope, $location, stateService, navService, responsesService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        // multiple choice responses
        let questionKeys = [ 'PayType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        let query = $location.search();

        var vm = this;
        vm.activeTab = query.t ? query.t : 1;
        vm.showLinks = false;
        vm.showAllHelp = false;

        vm.onTabClick = function(id) {
            vm.activeTab = id;

            vm.setNextTabQuery(id);
        }

        vm.setNextTabQuery = function(id) {
            if (id === 1) {
                navService.setNextQuery({ t: 2 }, "Next: Add Piece Rate");
            }
            else {
                navService.clearNextQuery();
            }
        }

        $scope.$watch('formData.payType', function(value) {
            if (value === 23 && vm.activeTab === 1) {
                vm.setNextTabQuery(1);
            }
            else {
                vm.setNextTabQuery();
            }
        });
  });
}
