(function () {
    'use strict';

    require('angular');
    require('angular-route');

    var app = angular.module('app', [
        'ngRoute',

        // 3rd Party Modules 

        // Custom modules

    ], function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', {
                templateUrl: 'app/dashboard/index.html',
                controller: 'Dashboard'
            });

        $locationProvider.html5Mode(true);

    });
    app.run([
        "$rootScope", "$window", '$location', 'CommonMethodsService', function ($rootScope, $window, $location, commonMethodsService) {

            $rootScope.$on('$routeChangeSuccess', function (evt, absNewUrl, absOldUrl) {
                $window.scrollTo(0, 0); //scroll to top of page after each route change
                commonMethodsService.lastPageRoute($location);
            });

        }
    ]);
    require('./daskboard/daskboard.controller.js');


})();