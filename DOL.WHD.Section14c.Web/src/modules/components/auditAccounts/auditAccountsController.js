'use strict';

module.exports = function(ngModule) {
  ngModule.controller('auditAccountsController', function(
    apiService,
    stateService
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.attachmentApiURL = apiService.attachmentApiURL;
    vm.access_token = stateService.access_token;


  });
};
