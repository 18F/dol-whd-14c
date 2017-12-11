describe('sectionAssurancesController', function() {
  beforeEach(function() {
    module('14c');
  });

  it('should map routes to controllers', function() {
    inject(function($route) {
      expect($route.routes['/'].controller).toBe('landingPageController');
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
      expect($route.routes['/admin/:app_id'].redirectTo({ app_id: 'app_id' })).toEqual('/admin/app_id/section/summary');

      var params = { app_id: 'app_id', section_id: 'section_id', item_id: 'item_id' };
      expect($route.routes['/admin/:app_id/section/:section_id'].template(params)).toEqual('<admin-review appid=app_id><section-admin-section_id item-id=item_id></section-admin-section_id></admin-review>');
      params = { app_id: 'app_id', section_id: 'section_id' };
      expect($route.routes['/admin/:app_id/section/:section_id'].template(params)).toEqual('<admin-review appid=app_id><section-admin-section_id item-id=></section-admin-section_id></admin-review>');

      // otherwise redirect to
      expect($route.routes[null].redirectTo).toEqual('/')
    });
  });

  describe('should set access on routes', function() {
    var routes = [
      { path: '/', access: 1 },
      { path: '/changePassword', access: 1 },
      { path: '/forgotPassword', access: 1 },
      { path: '/login', access: 1 },
      { path: '/register', access: 1 },
      { path: '/account/:userId', access: 3 },
      { path: '/section/assurances', access: 7 },
      { path: '/section/app-info', access: 7 },
      { path: '/section/employer', access: 7 },
      { path: '/section/wage-data', access: 7 },
      { path: '/section/work-sites', access: 7 },
      { path: '/section/wioa', access: 7 },
      { path: '/section/review', access: 7 },
      { path: '/admin', access: 11 },
      { path: '/admin/users', access: 11 },
      { path: '/admin/:app_id', access: 11 },
      { path: '/admin/:app_id/section/:section_id', access: 11 }
    ];
    routes.forEach(function(route) {
      it('sets the access on ' + route.path + ' to ' + route.access, function() {
        inject(function($route) {
          expect($route.routes[route.path].access).toBe(route.access);
        });
      });
    });
  });
});
