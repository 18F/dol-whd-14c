'use strict';

module.exports = function(ngModule) {
    ngModule.controller('sectionAppInfoController', function($scope, stateService, responsesService) {
        'ngInject';
        'use strict';

        $scope.formData = stateService.formData;

        // multiple choice responses
        let questionKeys = [ 'ApplicationType', 'EstablishmentType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => { $scope.responses = responses; });
    });
}
