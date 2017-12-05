describe('sectionAdminWageDataDirective', function() {
  beforeEach(module('14c'));

  var adminWageDataPayType, sectionAdminWageData, rootScope;
  beforeEach(function() {
    adminWageDataPayType = angular.element('<admin-wage-data-pay-type/>');
    sectionAdminWageData = angular.element('<section-admin-wage-data/>');
    inject(function($rootScope, $compile) {
      rootScope = $rootScope;
      $compile(adminWageDataPayType)(rootScope);
      $compile(sectionAdminWageData)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(adminWageDataPayType).toBeDefined();
    expect(sectionAdminWageData).toBeDefined();
  });
});
