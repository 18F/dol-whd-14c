'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionEmployerController', function($scope, stateService, apiService, responsesService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        if (!$scope.formData.employer) {
            $scope.formData.employer = {};
        }

        if (!$scope.formData.employer.numSubminimalWageWorkers) {
            $scope.formData.employer.numSubminimalWageWorkers = {
                total: 0,
                workCenter: 0,
                patientWorkers: 0,
                swep: 0,
                businessEstablishment: 0
            };
        }

        if (!$scope.formData.employer.providingFacilitiesDeductionTypeId) {
            $scope.formData.employer.providingFacilitiesDeductionTypeId = [];
        }

        // multiple choice responses
        let questionKeys = [ 'EmployerStatus', 'SCA', 'EO13658', 'ProvidingFacilitiesDeductionType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        var vm = this;
        vm.showAllHelp = false;
        vm.attachmentApiURL = apiService.attachmentApiURL + stateService.ein;
        vm.access_token = stateService.access_token;

        this.onHasTradeNameChange = function() {
            $scope.formData.employer.tradeName = '';
        }

        this.onHasLegalNameChange = function() {
            $scope.formData.employer.priorLegalName = '';
        }

        this.toggleDeductionType = function(id) {
            let index = $scope.formData.employer.providingFacilitiesDeductionTypeId.indexOf(id);
            if (index > -1) {
                $scope.formData.providingFacilitiesDeductionTypeId.splice(index, 1);
            }
            else {
                $scope.formData.providingFacilitiesDeductionTypeId.push(id);
            }
        }

        this.onSCAAttachmentSelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.formData.employer.SCAAttachment = result.data[0];
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
            }
        }

        this.deleteSCAAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.formData.employer.SCAAttachment = undefined;
            }, function(error){
                //TODO: Display error
                $scope.formData.employer.SCAAttachment = undefined;
            })
        }
  });
}
