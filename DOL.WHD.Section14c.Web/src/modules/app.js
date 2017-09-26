/* eslint-disable no-global-assign */
if (typeof console === 'undefined') {
  console = {
    log: function() {}
  };
}

// Global Dependencies
require('jquery');
require('font-awesome/css/font-awesome.css');
require('angular-data-grid/dist/dataGrid.min.js');
require('angular-data-grid/dist/pagination.min.js');

// Angular
import angular from 'angular';
import ngAnimate from 'angular-animate';
import ngResource from 'angular-resource';
import ngRoute from 'angular-route';
import ngSanitize from 'angular-sanitize';
import vcRecaptcha from 'angular-recaptcha';
import angularMoment from 'angular-moment';
import ngMask from 'ng-mask';
import ngCookies from 'angular-cookies';

// angular 4 components (& downgrade dependencies)
import { downgradeComponent } from '@angular/upgrade/static';
import { DolHeaderComponent } from '../v4/dol-header.component';
import { HelloWorldComponent } from '../v4/hello-world.component';
import { UiLibraryComponent } from '../v4/ui-library.component';

// Styles
import '../styles/main.scss';

// Angular application module
let app = angular.module('14c', [
  ngAnimate,
  ngResource,
  ngRoute,
  ngSanitize,
  'vcRecaptcha',
  'angularMoment',
  'ngMask',
  'ngCookies',
  'dataGrid',
  'pagination'
]);

app
  .directive('dolHeader', downgradeComponent({ component: DolHeaderComponent }))
  .directive(
    'helloWorld',
    downgradeComponent({ component: HelloWorldComponent })
  )
  .directive(
    'uiLibrary',
    downgradeComponent({ component: UiLibraryComponent })
  );

// Environment config loaded from env.js
let env = {};
if (window && window.__env) {
  Object.assign(env, window.__env);
}

app.constant('_env', env);

// Load Application Components
require('./constants')(app);
require('./components')(app);
require('./filters')(app);
require('./pages')(app);
require('./services')(app);

// route access states
const ROUTE_PUBLIC = 1;
const ROUTE_LOGGEDIN = 3;
const ROUTE_USER = 7;
const ROUTE_ADMIN = 11;

let checkRouteAccess = function(route, userAccess) {
  if (!route || !route.access) {
    return true;
  }

  return (route.access & userAccess) === route.access;
};

app.config(function($routeProvider, $compileProvider) {
  $routeProvider
    .when('/', {
      controller: 'landingPageController',
      reloadOnSearch: false,
      template: require('./pages/landingPageTemplate.html'),
      access: ROUTE_PUBLIC,
      isLanding: true
    })
    .when('/changePassword', {
      controller: 'changePasswordPageController',
      template: require('./pages/changePasswordPageTemplate.html'),
      access: ROUTE_PUBLIC
    })
    .when('/forgotPassword', {
      controller: 'forgotPasswordPageController',
      template: require('./pages/forgotPasswordPageTemplate.html'),
      access: ROUTE_PUBLIC
    })
    .when('/login', {
      controller: 'userLoginPageController',
      template: require('./pages/userLoginPageTemplate.html'),
      access: ROUTE_PUBLIC
    })
    .when('/register', {
      controller: 'userRegistrationPageController',
      template: require('./pages/userRegistrationPageTemplate.html'),
      access: ROUTE_PUBLIC
    })
    .when('/account/:userId', {
      controller: 'accountPageController',
      template: require('./pages/accountPageTemplate.html'),
      access: ROUTE_LOGGEDIN
    })
    .when('/section/:section_id', {
      template: function(params) {
        return (
          '<form-section><section-' +
          params.section_id +
          '></section-' +
          params.section_id +
          '></form-section>'
        );
      },
      reloadOnSearch: false,
      access: ROUTE_USER
    })
    .when('/admin', {
      controller: 'adminDashboardController',
      template: require('./pages/adminDashboardTemplate.html'),
      access: ROUTE_ADMIN
    })
    .when('/admin/users', {
      controller: 'userManagementPageController',
      template: require('./pages/userManagementPageTemplate.html'),
      access: ROUTE_ADMIN
    })
    .when('/admin/:app_id', {
      redirectTo: function(params) {
        return '/admin/' + params.app_id + '/section/summary';
      },
      access: ROUTE_ADMIN
    })
    .when('/admin/:app_id/section/:section_id', {
      template: function(params) {
        return (
          '<admin-review appid=' +
          params.app_id +
          '><section-admin-' +
          params.section_id +
          ' item-id=' +
          (params.item_id || '') +
          '></section-admin-' +
          params.section_id +
          '></admin-review>'
        );
      },
      reloadOnSearch: false,
      access: ROUTE_ADMIN
    })
    .when('/v4/hello', { template: '<hello-world></hello-world>' })
    .when('/v4/ui-library', { template: '<ui-library></ui-library>' })
    .otherwise({
      redirectTo: '/'
    });
});

/* eslint-disable complexity */
app.run(function(
  $rootScope,
  $location,
  stateService,
  autoSaveService,
  authService,
  $q
) {
  // check cookie to see if we're logged in
  const accessToken = stateService.access_token;
  let authenticatedPromise;
  if (accessToken) {
    // authenticate the user based on token
    authenticatedPromise = authService.authenticateUser();
    authenticatedPromise.then(undefined, function errorCallback(error) {
      console.log(error);
    });
  } else {
    const d = $q.defer();
    authenticatedPromise = d.promise;
    d.resolve();
  }

  //TODO: remove dev_flag check
  if (!env.dev_flag === true) {
    // watch for route changes and redirect non-public routes if not logged in
    $rootScope.$on('$routeChangeStart', function(event, next, current) {
      authenticatedPromise.then(function() {
        if (!next.$$route) {
          return;
        }

        let userAccess = stateService.isAdmin
          ? ROUTE_ADMIN
          : stateService.loggedIn ? ROUTE_USER : ROUTE_PUBLIC;
        if (!checkRouteAccess(next.$$route, userAccess)) {
          // user does not have adequate permissions to access the route so redirect
          $location.path('/' + (userAccess === ROUTE_ADMIN ? 'admin' : ''));
        } else if (next.$$route.isLanding && userAccess === ROUTE_ADMIN) {
          // redirect admin users to the admin dashboard
          $location.path('/admin');
        }
      });
    });
  }
});
/* eslint-enable complexity */

export { app };
