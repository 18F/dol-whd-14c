'use strict';

module.exports = function(ngModule) {
  ngModule.controller('dolHeaderController', function($scope, Idle) {
    'ngInject';
    'use strict';

    $scope.events = [];

  	$scope.$on('IdleStart', function() {
  		// the user appears to have gone idle
  	});

  	$scope.$on('IdleWarn', function(e, countdown) {
  		// follows after the IdleStart event, but includes a countdown until the user is considered timed out
  		// the countdown arg is the number of seconds remaining until then.
  		// you can change the title or display a warning dialog from here.
  		// you can let them resume their session by calling Idle.watch()
  	});

  	$scope.$on('IdleTimeout', function() {
  		// the user has timed out (meaning idleDuration + timeout has passed without any activity)
  		// this is where you'd log them
  	});

  	$scope.$on('IdleEnd', function() {
  		// the user has come back from AFK and is doing stuff. if you are warning them, you can use this to hide the dialog
  	});

  	$scope.$on('Keepalive', function() {
  		// do something to keep the user's session alive
  	});
  }).config(function(IdleProvider, KeepaliveProvider) {
  	// configure Idle settings
  	IdleProvider.idle(5); // in seconds
  	IdleProvider.timeout(5); // in seconds
  	KeepaliveProvider.interval(2); // in seconds
  })
  .run(function(Idle){
  	// start watching when the app runs. also starts the Keepalive service by default.
  	Idle.watch();
  });
};
