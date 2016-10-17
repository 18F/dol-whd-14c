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

app.config(function($routeProvider, $compileProvider) {
    $routeProvider
    .when('/', {
        controller: 'landingPageController',
        reloadOnSearch: false,
        template: require('./pages/landingPageTemplate.html'),
        public: true
    })
    .when('/changePassword', {
        controller: 'changePasswordPageController',
        template: require('./pages/changePasswordPageTemplate.html')
    })
    .when('/login', {
        controller: 'userLoginPageController',
        template: require('./pages/userLoginPageTemplate.html'),
        public: true
    })
    .when('/register', {
        controller: 'userRegistrationPageController',
        template: require('./pages/userRegistrationPageTemplate.html'),
        public: true
    })
    .when('/section/:section_id', {
        template: function(params){ return '<form-section><section-' + params.section_id + '></section-' + params.section_id + '></form-section>'; }
    })
    .otherwise({
        redirectTo: '/'
    });
});

app.run(function($rootScope, $location) {
    $rootScope.loadImage = function(image) {
        return require('../images/' + image);
    };

    //TODO: remove dev_flag check
    if (!env.dev_flag === true) {
        // watch for route changes and redirect non-public routes if not logged in
        $rootScope.$on( "$routeChangeStart", function(event, next, current) {
            if ( !$rootScope.loggedIn && next.$$route.public !== true ) {
                $location.path( "/" );
            }
        });
    }
});
