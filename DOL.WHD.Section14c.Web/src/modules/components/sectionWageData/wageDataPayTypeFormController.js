'use strict';

import merge from 'lodash/merge'

var moment = require('moment');

module.exports = function(ngModule) {
    ngModule.controller('wageDataPayTypeFormController', function($scope, stateService, apiService, responsesService, validationService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;
        vm.addingEmployer = false;
        vm.activeSourceEmployer = {};
        vm.activeSourceEmployerIndex = -1;

        $scope.formData = stateService.formData;
        $scope.validate = validationService.getValidationErrors;

        $scope.modelPrefix = function() {
            var prefix = 'hourlyWageInfo';
            if($scope.paytype === 'piecerate') {
                prefix = 'pieceRateWageInfo';
            }
            return prefix;
        };

        if (!$scope.formData[$scope.modelPrefix()]) {
            $scope.formData[$scope.modelPrefix()] = {
                numWorkers : 0,
                jobName : '',
                jobDescription : '',
                prevailingWageMethod : '',
                mostRecentPrevailingWageSurvey : {
                    basedOnSurvey : '',
                    sourceEmployers : [ ]
                }
            };
        }

        // multiple choice responses
        let questionKeys = [ 'PrevailingWageMethod' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        this.addSourceEmployer = function() {
            //vm.activeSourceEmployer.contactDate = moment(vm.activeSourceEmployer.contactDate.year + '-' + vm.activeSourceEmployer.contactDate.month + '-' + vm.activeSourceEmployer.contactDate.day).format();
            if (vm.activeSourceEmployerIndex > -1) {
                $scope.formData[$scope.modelPrefix()].mostRecentPrevailingWageSurvey.sourceEmployers[vm.activeSourceEmployerIndex] = vm.activeSourceEmployer;
            }
            else {
                $scope.formData[$scope.modelPrefix()].mostRecentPrevailingWageSurvey.sourceEmployers.push(vm.activeSourceEmployer);
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
            vm.activeSourceEmployer = merge({}, $scope.formData[$scope.modelPrefix()].mostRecentPrevailingWageSurvey.sourceEmployers[index]);
            vm.addingEmployer = true;
        }

        this.deleteSourceEmployer = function(index) {
            $scope.formData[$scope.modelPrefix()].mostRecentPrevailingWageSurvey.sourceEmployers.splice(index, 1);
        }

        this.validateSourceEmployer = function(index) {
            if (index < 0) {
                return undefined;
            }

            return validationService.getValidationErrors($scope.modelPrefix() +   '.mostRecentPrevailingWageSurvey.sourceEmployers[' + index + ']');
        }

        this.validateActiveSourceEmployerProperty = function(prop) {
            if (!prop || this.activeSourceEmployerIndex < 0) {
                return undefined;
            }

            return validationService.getValidationErrors($scope.modelPrefix() +   '.mostRecentPrevailingWageSurvey.sourceEmployers[' + this.activeSourceEmployerIndex + '].' + prop);
        }
  });
}
