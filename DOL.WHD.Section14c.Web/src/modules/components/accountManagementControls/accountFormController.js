'use strict';

module.exports = function(ngModule) {
    ngModule.controller('accountFormController', function($scope, $location, $routeParams, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;
        
        if($routeParams.userId !== "create"){
            $scope.userId = $routeParams.userId;
            vm.isEditAccount = true;
        }
        else {
            $scope.formVals = {
                roles: []
            }
        }
        
        apiService.getAccount(stateService.access_token, $scope.userId).then(function (result) {
            var data = result.data;
            $scope.formVals = data;
        }, function (error) {
            console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));

            //TODO: inform user of error
        });

        apiService.getRoles(stateService.access_token).then(function (result) {
            var data = result.data;
            $scope.roles = data;
        }, function (error) {
            console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));

            //TODO: inform user of error
        });

        vm.submitForm = function() {
            if(vm.isEditAccount){
                apiService.modifyAccount(stateService.access_token, $scope.formVals).then(function (result) {
                     $location.path("/");
                }, function (error) {
                    console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                    //TODO: inform user of error
                });
            } else {
                apiService.createAccount(stateService.access_token, $scope.formVals).then(function (result) {
                     $location.path("/");
                }, function (error) {
                    console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                    //TODO: inform user of error
                });
            }
        }


        vm.toggleRole = function(role) {
            var idx = vm.roleExists(role.id);
            if (idx > -1) {
                $scope.formVals.roles.splice(idx, 1);
            }
            else {
                $scope.formVals.roles.push(role);
            }
        }

        vm.roleExists = function(roleId) {
            var foundId = -1;
            for(var i = 0; i < $scope.formVals.roles.length; i++) {
                if ($scope.formVals.roles[i].id === roleId) {
                    foundId = i;
                    break;
                }
            }
            return foundId;
        }
  });
}