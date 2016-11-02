'use strict';

module.exports = function(ngModule) {
    ngModule.controller('accountGridController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        apiService.getAccounts(stateService.access_token).then(function (result) {
            var data = result.data;
            $scope.accounts = data;
        }, function (error) {
            console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));

            //TODO: inform user of error
        });

        vm.editAccountClick = function(userId) {
            $location.path("/account/" + userId);
        }

  });
}