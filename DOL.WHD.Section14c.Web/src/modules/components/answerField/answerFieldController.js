'use strict';

module.exports = function(ngModule) {
    ngModule.controller('answerFieldController', function(apiService, stateService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.attachmentApiURL = apiService.attachmentApiURL + stateService.ein;
        vm.access_token = stateService.access_token;
    });
}