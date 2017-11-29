'use strict';

import merge from 'lodash/merge';

module.exports = function(ngModule) {
  ngModule.controller('sectionWioaController', function(
    $scope,
    stateService,
    responsesService,
    validationService
  ) {
    'ngInject';
    'use strict';

    $scope.showAllHelp = false;     

    // multiple choice responses
    let questionKeys = ['WIOAWorkerVerified'];
    responsesService.getQuestionResponses(questionKeys).then(responses => {
      $scope.responses = responses;
    });

    $scope.formData = stateService.formData;
    $scope.validate = validationService.getValidationErrors;
    $scope.showWIOAReqs = false;
    if (!$scope.formData.WIOA) {
      $scope.formData.WIOA = { WIOAWorkers: [] };
    }

    var vm = this;
    vm.addingWorker = $scope.formData.WIOA.WIOAWorkers.length === 0;
    vm.activeWorker = {};
    vm.activeWorkerIndex = -1;

    vm.toggleLearnMore = function() {
      $scope.showWIOAReqs = !$scope.showWIOAReqs;
    };

    vm.addWorker = function() {
      if (vm.activeWorkerIndex > -1) {
        $scope.formData.WIOA.WIOAWorkers[vm.activeWorkerIndex] =
          vm.activeWorker;
      } else {
        $scope.formData.WIOA.WIOAWorkers.push(vm.activeWorker);
      }

      vm.cancelAddWorker();
    };

    vm.cancelAddWorker = function() {
      vm.activeWorker = {};
      vm.activeWorkerIndex = -1;
      vm.addingWorker = false;
    };

    vm.editWorker = function(index) {
      vm.activeWorkerIndex = index;
      vm.activeWorker = merge({}, $scope.formData.WIOA.WIOAWorkers[index]);
      vm.addingWorker = true;
    };

    vm.deleteWorker = function(index) {
      $scope.formData.WIOA.WIOAWorkers.splice(index, 1);
    };

    this.toggleAllHelpText = function() {
      $scope.showAllHelp = !$scope.showAllHelp;
    }    
  });
};
