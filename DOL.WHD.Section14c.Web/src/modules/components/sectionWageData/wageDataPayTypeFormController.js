'use strict';

import merge from 'lodash/merge'

var moment = require('moment');

module.exports = function(ngModule) {
    ngModule.controller('wageDataPayTypeFormController', function($scope, $location, stateService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;
        vm.addingEmployer = false;
        vm.activeSourceEmployer = {};
        vm.activeSourceEmployerIndex = -1;

        $scope.formData = stateService.formData;

        $scope.formData.wageTypeInfo = {
            numWorkers : 0,
            jobName : '',
            jobDescription : '',
            prevailingWageMethod : '',
            mostRecentPrevailingWageSurvey : {
                basedOnSurvey : '',
                sourceEmployers : [ ]
            }
        };

        this.onStudySelected = function(fileinput) {
            $scope.formData.workMeasurementStudyName = fileinput.files.length > 0 ? fileinput.files[0].name : '';
            if(!$scope.$$phase) {
                $scope.$apply();
            }
        }

        this.onHourlyDeterminationSelected = function(fileinput) {
            $scope.formData.hourlyDeterminationName = fileinput.files.length > 0 ? fileinput.files[0].name : '';
            if(!$scope.$$phase) {
                $scope.$apply();
            }
        }

        this.onSCAWageDeterminationSelected = function(fileinput) {
            $scope.formData.scaWageDeterminationName = fileinput.files.length > 0 ? fileinput.files[0].name : '';
            if(!$scope.$$phase) {
                $scope.$apply();
            }
        }

        this.onPieceRateDocumentSelected = function(fileinput) {
            $scope.formData.pieceRateDocumentName = fileinput.files.length > 0 ? fileinput.files[0].name : '';
            if(!$scope.$$phase) {
                $scope.$apply();
            }
        }

        this.addSourceEmployer = function() {
            //vm.activeSourceEmployer.contactDate = moment(vm.activeSourceEmployer.contactDate.year + '-' + vm.activeSourceEmployer.contactDate.month + '-' + vm.activeSourceEmployer.contactDate.day).format();
            if (vm.activeSourceEmployerIndex > -1) {
                $scope.formData.wageTypeInfo.mostRecentPrevailingWageSurvey.sourceEmployers[vm.activeSourceEmployerIndex] = vm.activeSourceEmployer;
            }
            else {
                $scope.formData.wageTypeInfo.mostRecentPrevailingWageSurvey.sourceEmployers.push(vm.activeSourceEmployer);
            }

            vm.cancelAddSourceEmployer();
        }

        this.cancelAddSourceEmployer = function() {
            vm.activeSourceEmployer = {};
            vm.activeSourceEmployerIndex = -1;
            vm.addingEmployer = false;
        }

        this.editSourceEmployer = function(index) {
            vm.activeSourceEmployerIndex = index;
            vm.activeSourceEmployer = merge({}, $scope.formData.wageTypeInfo.mostRecentPrevailingWageSurvey.sourceEmployers[index]);
            vm.addingEmployer = true;
        }

        this.deleteSourceEmployer = function(index) {
            $scope.formData.wageTypeInfo.mostRecentPrevailingWageSurvey.sourceEmployers.splice(index, 1);
        }
  });
}
