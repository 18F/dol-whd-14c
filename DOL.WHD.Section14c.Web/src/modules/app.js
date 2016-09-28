if (typeof console === 'undefined') {
    console = {
        log: function() { }
    };
}

// Global Dependencies
require('jquery');
require('lodash');

// Angular
import angular from 'angular';
import ngAnimate from 'angular-animate';
import ngResource from 'angular-resource';
import ngRoute from 'angular-route';
import ngSanitize from 'angular-sanitize';

// Styles
import '../styles/main.scss';

var app = angular.module('app', [
    ngAnimate,
    ngResource,
    ngRoute,
    ngSanitize,
]);

// Load Application Components
//require('./components')(app);
require('./pages')(app);
//require('./services')(app);

app.config(function($routeProvider) {
    $routeProvider
    .when('/', {
        controller: 'appMainPageController',
        reloadOnSearch: false,
        template: require('./pages/appMainPageTemplate.html')
    })
    .when('/about', {
        controller: 'appAboutPageController',
        template: require('./pages/appAboutPageTemplate.html')
    })
    .otherwise({
        redirectTo: '/'
    });
});
