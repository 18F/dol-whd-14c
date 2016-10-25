'use strict';

import merge from 'lodash/merge'

var moment = require('moment');

module.exports = function(ngModule) {
    ngModule.controller('wageDataPayTypeFormController', function($scope, stateService, apiService, responsesService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;
        vm.addingEmployer = false;
        vm.activeSourceEmployer = {};
        vm.activeSourceEmployerIndex = -1;
        vm.attachmentApiURL = apiService.attachmentApiURL + stateService.ein;
        vm.access_token = stateService.access_token;

        $scope.formData = stateService.formData;

        if (!$scope.formData.wageTypeInfo) {
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
        }

        $scope.modelPrefix = function() {
            var prefix = 'hourlyWageInfo';
            if($scope.paytype === 'piecerate') {
                prefix = 'pieceRateWageInfo';
            }
            return prefix;
        };

        // multiple choice responses
        let questionKeys = [ 'PrevailingWageMethod' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        this.onStudySelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.formData.workMeasurementStudyAttachment = result.data[0];
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
            }
        }

        this.onHourlyDeterminationSelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.formData.hourlyDeterminationAttachment = result.data[0];
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
            }
        }

        this.onSCAWageDeterminationSelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.formData.scaWageDeterminationAttachment = result.data[0];
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
            }
        }

        this.onPieceRateDocumentSelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.formData.pieceRateDocumentAttachment = result.data[0];
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
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

        this.deleteWorkMeasurementStudyAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.formData.workMeasurementStudyAttachment = undefined;
            }, function(error){
                //TODO: Display error
                $scope.formData.workMeasurementStudyAttachment = undefined;
            })
        }

        this.deleteHourlyDeterminationAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.formData.hourlyDeterminationAttachment = undefined;
            }, function(error){
                //TODO: Display error
                $scope.formData.hourlyDeterminationAttachment = undefined;
            })
        }
        this.deleteScaWageDeterminationAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.formData.scaWageDeterminationAttachment = undefined;
            }, function(error){
                //TODO: Display error
                $scope.formData.scaWageDeterminationAttachment = undefined;
            })
        }
        this.deletePieceRateDocumentAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.formData.pieceRateDocumentAttachment = undefined;
            }, function(error){
                //TODO: Display error
                $scope.formData.pieceRateDocumentAttachment = undefined;
            })
        }
  });
}
