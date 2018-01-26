'use strict';

module.exports = function(ngModule) {
  ngModule.controller('dolHeaderController', function($scope, Idle, stateService, $location) {
    'ngInject';
    'use strict';
    $scope.modalIsVisible=false;

    $scope.$on('IdleWarn',function(){
      $scope.showIdleWarning();
    });

    $scope.$on('IdleTimeout',function(){
      $scope.hideIdleWarning();
      Idle.unwatch();
      stateService.logOut();
      $location.path('/login').search({timeout:true});
    });

    $scope.showIdleWarning=function(){
      $scope.modalIsVisible=true;
    };

    $scope.hideIdleWarning  = function(){
      Idle.watch();
      $scope.modalIsVisible=false;
    }
  });
};
