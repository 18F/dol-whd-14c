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
import angularMoment from 'angular-moment';
import ngMask from 'ng-mask';
import toastr from 'angular-toastr';
import ngCookies from 'angular-cookies';
// angular 4 components (& downgrade dependencies)
import { downgradeComponent, downgradeInjectable } from '@angular/upgrade/static';
import { DolFooterComponent } from '../v4/dol-footer.component';
import { DolHeaderComponent } from '../v4/dol-header.component';
import { HelloWorldComponent } from '../v4/hello-world.component';
import { UiLibraryComponent } from '../v4/ui-library.component';
import { LoggingService } from '../v4/services/logging.service';

import { customError } from '../models/customError';

// Styles
import '../styles/main.scss';

// Angular application module
let app = angular.module('14c', [
  ngAnimate,
  ngResource,
  ngRoute,
  ngSanitize,
  toastr,
  require('angular-crumble'),
  'angularMoment',
  'ngMask',
  'ngCookies',
  'dataGrid',
  'pagination'
]);

// augment angular exception handler
app.provider('$exceptionHandler', { $get: function( errorLogService ) {
  return( errorLogService );
}});

app
  .directive('dolFooter', downgradeComponent({ component: DolFooterComponent }))
  //.directive('dolHeader', downgradeComponent({ component: DolHeaderComponent }))
  .directive('helloWorld', downgradeComponent({ component: HelloWorldComponent }))
  .directive('uiLibrary', downgradeComponent({ component: UiLibraryComponent }))
  .factory('loggingService', downgradeInjectable(LoggingService));

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

require('./routes')(app)

/* eslint-disable complexity */
app.run(function(
  $rootScope,
  $location,
  $log,
  crumble,
  stateService,
  autoSaveService,
  authService,
  $q
) {

  var getParent = crumble.getParent;
  crumble.getParent = function (path) {
    var route = crumble.getRoute(path);
    return route && angular.isDefined(route.parent) ? route.parent : getParent(path);
  };

  // check cookie to see if we're logged in
  const accessToken = stateService.access_token;
  let authenticatedPromise;
  if (accessToken) {
    // authenticate the user based on token
    authenticatedPromise = authService.authenticateUser();
    authenticatedPromise.then(function(response) {
      $log.info('Succssfully authenticated user and got saved application.')
    }).catch(function(error){
      $log.warn('Error in authenticating user or getting saved application. This warning will appear if the user does not currently have a saved application.', error)
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
      }).catch(function(error){
        $log.warn('Error in authenticating user or getting saved application.', error)
      });
    });
  }
});
/* eslint-enable complexity */

export { app };
