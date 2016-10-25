'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionAppInfoController', function($scope, stateService, responsesService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        if(!$scope.formData.establishmentType) $scope.formData.establishmentType = [];

        // multiple choice responses
        let questionKeys = [ 'ApplicationType', 'EstablishmentType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });

        $scope.toggleEstablishmentType = function(id) {
            var idx = $scope.formData.establishmentType.indexOf(id);
            if (idx > -1) {
                $scope.formData.establishmentType.splice(idx, 1);
            }
            else {
                $scope.formData.establishmentType.push(id);
            }
        }

    });
}
