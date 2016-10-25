'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionEmployerController', function($scope, stateService, apiService, responsesService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        
        if(!$scope.formData.employerInfo) {
            $scope.formData.employerInfo = { providingFacilitiesDeductionType: [] };
        }

        // multiple choice responses
        let questionKeys = [ 'EmployerStatus', 'SCA', 'EO13658', 'ProvidingFacilitiesDeductionType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        var vm = this;
        vm.showAllHelp = false;
        vm.attachmentApiURL = apiService.attachmentApiURL + stateService.ein;
        vm.access_token = stateService.access_token;    

        this.onHasTradeNameChange = function() {
            $scope.formData.employerInfo.tradeName = '';
        }

        this.onHasLegalNameChange = function() {
            $scope.formData.employerInfo.priorLegalName = '';
        }

        this.onSCAAttachmentSelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.formData.employerInfo.SCAAttachment = result.data[0];
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
            }
        }

        this.deleteSCAAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.formData.employerInfo.SCAAttachment = undefined;
            }, function(error){
                //TODO: Display error
                $scope.formData.employerInfo.SCAAttachment = undefined;
            })
        }      

        this.toggleProvidingFacilitiesDeductionType = function(id) {
            var idx = $scope.formData.employerInfo.providingFacilitiesDeductionType.indexOf(id);
            if (idx > -1) {
                $scope.formData.employerInfo.providingFacilitiesDeductionType.splice(idx, 1);
            }
            else {
                $scope.formData.employerInfo.providingFacilitiesDeductionType.push(id);
            }
        }
  });
}
