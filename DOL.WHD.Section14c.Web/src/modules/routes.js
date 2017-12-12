'use strict';
var config = require('./routes.config');

module.exports = function (app) {

  app.config(function($routeProvider) {
    $routeProvider
      .when('/', {
        controller: 'homePageController',
        reloadOnSearch: false,
        template: require('./pages/homePageTemplate.html'),
        access: config.access.ROUTE_PUBLIC,
        isLanding: true,
        label: '14(c) Home'
      })
      .when('/dashboard', {
        controller: 'dashboardController',
        reloadOnSearch: false,
        template: require('./pages/dashboardTemplate.html'),
        access: config.access.ROUTE_LOGGEDIN,
        label: 'Dashboard'
      })
      .when('/changePassword', {
        controller: 'changePasswordPageController',
        template: require('./pages/changePasswordPageTemplate.html'),
        access: config.access.ROUTE_PUBLIC,
        label: 'Change Password',
        parent: '/dashboard'
      })
      .when('/forgotPassword', {
        controller: 'forgotPasswordPageController',
        template: require('./pages/forgotPasswordPageTemplate.html'),
        access: config.access.ROUTE_PUBLIC,
        label: 'Forgot Password',
        parent: '/'
      })
      .when('/login', {
        controller: 'userLoginPageController',
        templateUrl: './pages/userLoginPageTemplate.html',
        access: config.access.ROUTE_PUBLIC
      })
      .when('/register', {
        controller: 'userRegistrationPageController',
        template: require('./pages/userRegistrationPageTemplate.html'),
        access: config.access.ROUTE_PUBLIC
      })
      .when('/account/:userId', {
        controller: 'accountPageController',
        template: require('./pages/accountPageTemplate.html'),
        access: config.access.ROUTE_LOGGEDIN
      })
      .when('/section/assurances', {
        template: '<form-section><section-assurances></section-assurances></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Assurances',
        parent:'/'
      })
      .when('/section/app-info', {
        template: '<form-section><section-app-info></section-app-info></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Application Info',
        parent:'/'
      })
      .when('/section/employer', {
        template: '<form-section><section-employer></section-employer></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Employer',
        parent:'/'
      })
      .when('/section/wage-data', {
        template: '<form-section><section-wage-data></section-wage-data></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Wage Data',
        parent:'/'
      })
      .when('/section/work-sites', {
        template: '<form-section><section-work-sites></section-work-sites></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Work Sites & Employees',
        parent:'/'
      })
      .when('/section/wioa', {
        template: '<form-section><section-wioa></section-wioa></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'WIOA',
        parent:'/'
      })
      .when('/section/review', {
        template: '<form-section><section-review></section-review></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Review',
        parent:'/'
      })
      .when('/admin', {
        controller: 'adminDashboardController',
        templateUrl: './pages/adminDashboardTemplate.html',
        access: config.access.ROUTE_ADMIN
      })
      .when('/admin/users', {
        controller: 'userManagementPageController',
        template: require('./pages/userManagementPageTemplate.html'),
        access: config.access.ROUTE_ADMIN
      })
      .when('/admin/:app_id', {
        redirectTo: function(params) {
          return '/admin/' + params.app_id + '/section/summary';
        },
        access: config.access.ROUTE_ADMIN
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
        access: config.access.ROUTE_ADMIN
      })
      .when('/v4/hello', { template: '<hello-world></hello-world>' })
      .when('/v4/ui-library', { template: '<ui-library></ui-library>' })
      .otherwise({
        redirectTo: '/'
      });
  });
}
