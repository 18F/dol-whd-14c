'use strict';

module.exports = function(ngModule) {
    ngModule.controller('attachmentFieldController', function($scope, apiService, stateService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.attachmentApiURL = apiService.attachmentApiURL + stateService.ein;
        vm.access_token = stateService.access_token;
        
        this.onAttachmentSelected = function(fileinput) {
            if(fileinput.files.length > 0){
                apiService.uploadAttachment(stateService.access_token, stateService.ein, fileinput.files[0]).then(function (result){
                    $scope.attachmentId = result.data[0].id;
                    $scope.attachmentName = result.data[0].originalFileName;
                    fileinput.value = '';
                }, function(error){
                    //TODO: Display error
                    fileinput.value = '';
                })
            }
        };

        this.deleteAttachment = function(id){
            apiService.deleteAttachment(stateService.access_token, stateService.ein, id).then(function (result){
               $scope.attachmentId = undefined;
               $scope.attachmentName = undefined;
            }, function(error){
                //TODO: Display error
                $scope.attachmentId = undefined;
                $scope.attachmentName = undefined;
            })
        };
    });
}