

describe('sectionAssurancesController', function() {

  it('should map routes to controllers', function() {
  module('14c');

  inject(function($route) {

    expect($route.routes['/'].controller).toBe('landingPageController');
    expect($route.routes['/'].templateUrl).toEqual('./pages/landingPageTemplate.html');
    expect($route.routes['/changePassword'].templateUrl).toEqual('./pages/changePasswordPageTemplate.html');
    expect($route.routes['/changePassword'].controller).toEqual('changePasswordPageController');
    expect($route.routes['/forgotPassword'].templateUrl).toEqual('./pages/forgotPasswordPageTemplate.html');
    expect($route.routes['/forgotPassword'].controller).toEqual('forgotPasswordPageController');
    expect($route.routes['/login'].templateUrl).toEqual('./pages/userLoginPageTemplate.html');
    expect($route.routes['/login'].controller).toEqual('userLoginPageController');
    expect($route.routes['/register'].templateUrl).toEqual('./pages/userRegistrationPageTemplate.html');
    expect($route.routes['/register'].controller).toEqual('userRegistrationPageController');
    expect($route.routes['/account/:userId'].templateUrl).toEqual('./pages/accountPageTemplate.html');
    expect($route.routes['/account/:userId'].controller).toEqual('accountPageController');
    expect($route.routes['/admin/users'].templateUrl).toEqual('./pages/userManagementPageTemplate.html');
    expect($route.routes['/admin/users'].controller).toEqual('userManagementPageController');
    expect($route.routes['/section/assurances'].template).toEqual('<form-section><section-assurances></section-assurances></form-section>');
    expect($route.routes['/section/app-info'].template).toEqual('<form-section><section-app-info></section-app-info></form-section>');
    expect($route.routes['/section/employer'].template).toEqual('<form-section><section-employer></section-employer></form-section>');
    expect($route.routes['/section/wage-data'].template).toEqual('<form-section><section-wage-data></section-wage-data></form-section>');
    expect($route.routes['/section/work-sites'].template).toEqual('<form-section><section-work-sites></section-work-sites></form-section>');
    expect($route.routes['/section/wioa'].template).toEqual('<form-section><section-wioa></section-wioa></form-section>');
    expect($route.routes['/section/review'].template).toEqual('<form-section><section-review></section-review></form-section>');

    // otherwise redirect to
    expect($route.routes[null].redirectTo).toEqual('/')
  });
});
});
