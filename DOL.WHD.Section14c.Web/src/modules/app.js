if (typeof console === 'undefined') {
    console = {
        log: function() { }
    };
}

// Global Dependencies
require('jquery');
//require('lodash');

// Angular
import angular from 'angular';
import ngAnimate from 'angular-animate';
import ngResource from 'angular-resource';
import ngRoute from 'angular-route';
import ngSanitize from 'angular-sanitize';

// Styles
import '../styles/main.scss';

// Angular application module
let app = angular.module('14c', [
    ngAnimate,
    ngResource,
    ngRoute,
    ngSanitize
]);

// Environment config loaded from env.js
let env = {};
if (window && window.__env) {
    Object.assign(env, window.__env);
}

app.constant('_env', env);

// Load Application Components
require('./components')(app);
require('./pages')(app);
require('./services')(app);

app.config(function($routeProvider) {
    $routeProvider
    .when('/', {
        controller: 'landingPageController',
        reloadOnSearch: false,
        template: require('./pages/landingPageTemplate.html')
    })
    .when('/changePassword', {
        controller: 'changePasswordPageController',
        template: require('./pages/changePasswordPageTemplate.html')
    })
    .when('/login', {
        controller: 'userLoginPageController',
        template: require('./pages/userLoginPageTemplate.html')
    })
    .when('/register', {
        controller: 'userRegistrationPageController',
        template: require('./pages/userRegistrationPageTemplate.html')
    })
    .otherwise({
        redirectTo: '/'
    });
});
