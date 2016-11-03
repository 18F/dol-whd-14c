'use strict';

module.exports = function(ngModule) {
    ngModule.controller('accountFormController', function($scope, $location, $routeParams, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.stateService = stateService;

        vm.resetErrors = function() {
            vm.savingError = false;
            vm.loadingError = false;
        }
        
        vm.resetErrors();

        if($routeParams.userId !== "create"){
            $scope.userId = $routeParams.userId;
            vm.isEditAccount = true;
            apiService.getAccount(stateService.access_token, $scope.userId).then(function (result) {
                var data = result.data;
                $scope.formVals = data;
            }, function (error) {
                console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                vm.loadingError = true;
            });
        }
        else {
            $scope.formVals = {
                roles: []
            }
        }

        apiService.getRoles(stateService.access_token).then(function (result) {
            var data = result.data;
            $scope.roles = data;
        }, function (error) {
            console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
            vm.loadingError = true;
        });

        vm.submitForm = function() {
            vm.resetErrors();
            if(vm.isEditAccount){
                apiService.modifyAccount(stateService.access_token, $scope.formVals).then(function (result) {
                     $location.path("/");
                }, function (error) {
                    console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                    $scope.savingErrors = apiService.parseErrors(error.data);
                    vm.savingError = true;
                });
            } else {
                apiService.createAccount(stateService.access_token, $scope.formVals).then(function (result) {
                     $location.path("/");
                }, function (error) {
                    console.log(error.statusText + (error.data && error.data.error ? ': ' + error.data.error + ' - ' + error.data.error_description : ''));
                    $scope.savingErrors = apiService.parseErrors(error.data);
                    vm.savingError = true;
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

        vm.cancelClick = function() {
            $location.path("/");
        }
  });
}