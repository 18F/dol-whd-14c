'use strict';

module.exports = function(ngModule) {
  ngModule.controller('accountFormController', function(
    $scope,
    $location,
    $routeParams,
    stateService,
    apiService,
    moment,
    $window
  ) {
    'ngInject';
    'use strict';

    var vm = this;
    vm.stateService = stateService;
    vm.moment = moment;
    $scope.roles =null;
    vm.tempErrors = [];
    
    vm.resetErrors = function() {
      vm.savingError = false;
      vm.loadingError = false;
    };

    vm.setErrorObject = function() {
      $scope.savingErrors = vm.tempErrors;
      if (vm.savingError) {
        $window.scrollTo(0, 0);              
      }     
    };

    vm.resetErrors();

    if ($routeParams.userId !== 'create') {
      $scope.userId = $routeParams.userId;
      vm.isEditAccount = true;
      apiService.getAccount(stateService.access_token, $scope.userId).then(
        function(result) {
          var data = result.data;
          $scope.formVals = data;
        },
        function() {
          vm.loadingError = true;
        }
      );
    } else {
      $scope.formVals = {
        roles: []
      };
    }

    apiService.getRoles(stateService.access_token).then(
      function(result) {
        var data = result.data;
        $scope.roles = data;
      },
      function() {
        vm.loadingError = true;
      }
    );
    
    vm.submitForm = function() {
      vm.resetErrors();      

      if(!$scope.formVals.email) { 
        vm.tempErrors.push('Email is required.');       
        vm.savingError = true;
      }     

      if ($scope.formVals.roles.length === 0) {
        vm.tempErrors.push('At least one role is required.');
        vm.savingError = true;        
      }

      if (vm.savingError) {
        vm.setErrorObject();        
        return;
      }

      if (vm.isEditAccount) {
        apiService
          .modifyAccount(stateService.access_token, $scope.formVals)
          .then(
            function() {
              $location.path('/admin/users');
            },
            function(error) {
              $scope.savingErrors = apiService.parseErrors(error.data);
              vm.savingError = true;
            }
          );
      } else {
        apiService
          .createAccount(stateService.access_token, $scope.formVals)
          .then(
            function() {
              $location.path('/admin/users');
            },
            function(error) {
              $scope.savingErrors = apiService.parseErrors(error.data);

              for (var i = 0; i < $scope.savingErrors.length; i++) {
                vm.tempErrors.push($scope.savingErrors[i]);
                }             
              vm.savingError = true;
              vm.setErrorObject();
            }
          );
        }
    };

    vm.toggleRole = function(role) {
      var idx = vm.roleExists(role.id);
      if (idx > -1) {
        $scope.formVals.roles.splice(idx, 1);
      } else {
        $scope.formVals.roles.push(role);
      }
    };

    vm.roleExists = function(roleId) {
      var foundId = -1;
      for (var i = 0; i < $scope.formVals.roles.length; i++) {
        if ($scope.formVals.roles[i].id === roleId) {
          foundId = i;
          break;
        }
      }
      return foundId;
    };

    vm.cancelClick = function() {
      $location.path('/admin/users');
    };

    vm.update = {
      status: "Inprogress",
      message: 'In Progress ...'
    };
    vm.updateUserAccountModalIsVisible = false;

    vm.setupdateStatus = function (status, message) {
      vm.update.status = status;
      vm.update.message = message;
    };

    vm.showupdateUserAccountConfirmationModal = function () {
      vm.updateUserAccountModalIsVisible = true;
      vm.setupdateStatus('Initialize', 'Are you sure you want to update user account?');
    };

    vm.hideUpdateUserAccountConfirmationModal = function() {
      vm.updateUserAccountModalIsVisible = false;
    }


  });
};
