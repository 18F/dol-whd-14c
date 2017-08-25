'use strict';

module.exports = function(ngModule) {
    ngModule.controller('accountCreateButtonController', function($scope, $location) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.createAccountClick = function() {
            $location.path("/account/create");
        }

  });
}