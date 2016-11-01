'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionAppInfoController', function($scope, stateService, responsesService, validationService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;
        $scope.validate = validationService.getValidationErrors;

        if (!$scope.formData.establishmentType) {
            $scope.formData.establishmentType = [];
        }

        // multiple choice responses
        let questionKeys = [ 'ApplicationType', 'EstablishmentType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        let vm = this;

        this.toggleEstablishmentType = function(id) {
            let index = $scope.formData.establishmentType.indexOf(id);
            if (index > -1) {
                $scope.formData.establishmentType.splice(index, 1);
            }
            else {
                $scope.formData.establishmentType.push(id);
            }
        }
    });
}
