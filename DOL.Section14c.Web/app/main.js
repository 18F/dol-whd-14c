(function () {
    'use strict';

    require('angular');
    require('angular-animate');
    require('angular-route');

    // app scripts
    var exampleService = require('./services/exampleService.js');
    var dataService = require('./services/dataService.js');

    var dashboardController = require('./dashboard/dashboard.controller.js');

    var app = angular.module('app', [
        'ngRoute'

        // 3rd Party Modules  

        // Custom modules

    ], function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', {
                templateUrl: './dashboard/index.html',
                controller: 'DashboardController'
            })
            .otherwise({
                redirectTo: '/'
            });


        $locationProvider.html5Mode(true);

    });
    app.run([
            "$rootScope", "$window", '$location', function($rootScope, $window) {

                $rootScope.$on('$routeChangeSuccess',
                    function() {
                        $window.scrollTo(0, 0); //scroll to top of page after each route change
                    });
            }
    ])
    // Modules and Services
    .factory('ExampleService', ['$log', exampleService])
    .factory('DataService', ['$http', dataService])

    // Controllers
    .controller('DashboardController', ['$scope', 'DataService', dashboardController]);

})();