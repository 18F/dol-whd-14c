describe('sectionAdminSummaryDirective', function() {
  beforeEach(module('14c'));

  var sectionAdminSummary, rootScope;
  beforeEach(function() {
    sectionAdminSummary = angular.element('<section-admin-summary/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(sectionAdminSummary)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(sectionAdminSummary).toBeDefined();
  });
});
