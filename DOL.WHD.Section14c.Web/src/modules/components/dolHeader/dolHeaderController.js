'use strict';

module.exports = function(ngModule) {
  ngModule.controller('dolHeaderController', function($scope, Idle, stateService, $location) {
    'ngInject';
    'use strict';

    $scope.events=[];
    $scope.modalIsVisible=false;
    console.log($scope)
    $scope.$on('IdleWarn',function(){
      $scope.showModal();
    });

    $scope.$on('IdleTimeout',function(){
      $scope.hideModal();
      stateService.logOut();
      $location.path('/login').search({timeout:true});
    });

    $scope.showModal=function(){

      $scope.modalIsVisible=true;
    };

    $scope.resume = function(){
      Idle.watch();
      $scope.hideModal();
    };

    $scope.hideModal  = function(){
      $scope.modalIsVisible=false;
    }
  })
};
