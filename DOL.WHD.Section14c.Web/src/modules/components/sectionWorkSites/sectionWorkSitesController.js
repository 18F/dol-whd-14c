'use strict';

import isEmpty from 'lodash/isEmpty'

module.exports = function(ngModule) {
    ngModule.controller('sectionWorkSitesController', function($scope, $location, navService, responsesService, stateService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        if (!$scope.formData.workSites) {
            $scope.formData.workSites = [];
        }

        let query = $location.search();

        var vm = this;
        vm.activeTab = query.t ? query.t : 1;
        vm.activeWorksite = {};
        vm.activeWorksiteIndex = -1;
        vm.activeWorker = {};
        vm.activeWorkerIndex = -1;

        // multiple choice responses
        let questionKeys = [ 'WorkSiteType', 'PrimaryDisability' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        this.clearActiveWorker = function() {
            vm.activeWorker = {};
            vm.activeWorkerIndex = -1;
        }

        this.addEmployee = function() {
            if (!vm.activeWorksite || isEmpty(vm.activeWorker)) {
                return;
            }

            if (!vm.activeWorksite.employees) {
                vm.activeWorksite.employees = [];
            }

            if (vm.activeWorkerIndex > -1) {
                vm.activeWorksite.employees[vm.activeWorkerIndex] = vm.activeWorker;
            }
            else {
                vm.activeWorksite.employees.push(vm.activeWorker);
            }

            vm.clearActiveWorker();
        }

        this.addAnotherEmployee = function() {
            vm.addEmployee();
        }

        this.doneAddingEmployees = function() {
            vm.addEmployee();
            vm.addingEmployee = false;
        }

        this.editEmployee = function(index) {
            if (vm.activeWorksite && vm.activeWorksite.employees.length > index) {
                vm.activeWorkerIndex = index;
                vm.activeWorker = merge({}, vm.activeWorksite.employees[index]);
                vm.addingEmployee = true;
            }
        }

        this.deleteEmployee = function(index) {
            if (vm.activeWorkSite && vm.activeWorkSite.employees.length > index) {
                vm.activeWorkSite.employees.splice(index, 1);
            }
        }

        this.clearActiveWorkSite = function() {
            vm.activeWorksite = {};
            vm.activeWorksiteIndex = -1;
        }

        this.addWorkSite = function() {
            vm.addingWorkSite = true;
            vm.setActiveTab(1);
        }

        this.saveWorkSite = function() {
            if (!isEmpty(vm.activeWorksite)) {
                if (vm.activeWorksiteIndex > -1) {
                    $scope.formData.workSites[vm.activeWorksiteIndex] = vm.activeWorksite;
                }
                else {
                    $scope.formData.workSites.push(vm.activeWorksite);
                }
            }

            vm.clearActiveWorker();
            vm.clearActiveWorkSite();
            vm.addingWorkSite = false;
        }

        this.editWorkSite = function(index) {
            if ($scope.formData.workSites.length > index) {
                vm.activeWorksiteIndex = index;
                vm.activeWorksite = merge({}, $scope.formData.workSites[index]);
                vm.addingWorkSite = true;
            }
        }

        this.deleteWorkSite = function(index) {
            if ($scope.formData.workSites.length > index) {
                $scope.formData.workSites.splice(index, 1);
            }
        }

        this.setActiveTab = function(tab) {
            if (!vm.addingWorkSite || tab < 1 || tab > 2) {
                return;
            }

            vm.activeTab = tab;

            if (tab === 1) {
                navService.setNextQuery({ t: 2 }, "Next: Add Employee(s)");
                navService.setBackQuery({ doCancel: true }, "Cancel");
            }
            else {
                navService.setNextQuery({ doSave: true }, "Save Work Site & Employee(s)");
                navService.setBackQuery({ doCancel: true }, "Cancel");
            }
        }

        $scope.$on('$routeUpdate', function(){
            query = $location.search();
            if (query.t) {
                vm.setActiveTab(query.t);
            }

            if (query.doSave) {
                vm.saveWorkSite();
                navService.clearQuery();
            }

            if (query.doCancel) {
                vm.clearActiveWorker();
                vm.clearActiveWorkSite();
                vm.addingWorkSite = false;
                navService.clearQuery();
            }
        });
  });
}
