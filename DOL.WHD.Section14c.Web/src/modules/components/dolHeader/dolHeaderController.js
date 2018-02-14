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

    $scope.showIdleWarning=function() {
      if(!$scope.modalIsVisible) {
        var duration = Idle.getTimeout();
        // accounts for 1 second delay
        $scope.startTimer(duration-1);
      }
      $scope.modalIsVisible=true;
    };

    $scope.hideIdleWarning  = function(){
      $scope.modalIsVisible=false;
    }

    $scope.continueWorking = function () {
      Idle.watch();
      $scope.modalIsVisible=false;
    }

    $scope.startTimer = function (duration) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10)
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        $scope.timer = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = duration;
        }
    }, 1000);
  }

  });
};
