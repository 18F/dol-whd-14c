'use strict';
var config = require('./routes.config');

module.exports = function (app) {

  app.config(function($routeProvider) {
    $routeProvider
      .when('/', {
        controller: 'landingPageController',
        reloadOnSearch: false,
        template: require('./pages/landingPageTemplate.html'),
        access: config.access.ROUTE_PUBLIC,
        isLanding: true,
        label: 'Home'
      })
      .when('/changePassword', {
        controller: 'changePasswordPageController',
        template: require('./pages/changePasswordPageTemplate.html'),
        access: config.access.ROUTE_PUBLIC,
        label: 'Change Password',
        parent: '/'
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
        label: 'Section: Assurances',
        parent:'/'
      })
      .when('/section/app-info', {
        template: '<form-section><section-app-info></section-app-info></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Section: Application Info',
        parent:'/section/assurances'
      })
      .when('/section/employer', {
        template: '<form-section><section-employer></section-employer></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Section: Employer',
        parent:'/section/app-info'
      })
      .when('/section/wage-data', {
        template: '<form-section><section-wage-data></section-wage-data></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Section: Wage Data',
        parent:'/section/employer'
      })
      .when('/section/work-sites', {
        template: '<form-section><section-work-sites></section-work-sites></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Section: Work Sites & Employees',
        parent:'/section/wage-data'
      })
      .when('/section/wioa', {
        template: '<form-section><section-wioa></section-wioa></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Section: WIOA',
        parent:'/section/work-sites'
      })
      .when('/section/review', {
        template: '<form-section><section-review></section-review></form-section>',
        reloadOnSearch: false,
        access: config.access.ROUTE_USER,
        label: 'Section: Review',
        parent:'/section/wioa'
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
