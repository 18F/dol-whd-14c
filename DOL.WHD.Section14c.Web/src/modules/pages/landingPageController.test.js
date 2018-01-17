describe('landingPageController', function() {
  var scope, landingPageController, organizations, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, stateService, _$q_, $location) {
      scope = $rootScope.$new();
      mockStateService = stateService;
      $q = _$q_;
      location = $location

      landingPageController = function() {
        return $controller('landingPageController', {
          $scope: scope,
          stateService: mockStateService
        });
      };
      controller = landingPageController();
      scope.organizations = [
        {
          employer: {
            id: '1234'
          },
          applicationStatus: {
            name: "New"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        },
        {
          employer: {
            id: '12313'
          },
          applicationStatus: {
            name: "InProgress"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        },
        {
          employer: {
            id: '1234'
          },
          applicationStatus: {
            name: "Submitted"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        },
        {
          employer: {
            id: '3333'
          },
          applicationStatus: {
            name: "Invalid"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        }
      ];

      saveNewApplication = $q.defer();
      spyOn(mockStateService, 'saveNewApplication').and.returnValue(
        saveNewApplication.promise
      );

      loadSavedApplication = $q.defer();
      spyOn(mockStateService, 'loadSavedApplication').and.returnValue(
        loadSavedApplication.promise
      );

      downloadApplicationPdf = $q.defer();
      spyOn(mockStateService, 'downloadApplicationPdf').and.returnValue(
        downloadApplicationPdf.promise
      );
    })
  );

  it('invoke controller', function() {
    expect(controller).toBeDefined();
    expect(document.title).toBe('DOL WHD Section 14(c)');
  });

  it('invalid application status causes error', function() {
    scope.onApplicationClick(3);
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(true);
    expect(mockStateService.employerId).toEqual('3333');
    expect(mockStateService.applicationId).toEqual('1231541515');
    expect(mockStateService.ein).toEqual('12-123345');
  });

  it('saves new application with New status name', function() {
    scope.onApplicationClick(0);
    saveNewApplication.resolve();
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(false);
  });

  it('saveNewApplication reject causes error', function() {
    scope.onApplicationClick(0);
    saveNewApplication.reject({data: 'error'});
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(true);
    expect(scope.applicationLoadError.message).toBe('error');
  });

  it('loads new application with InProgress status name', function() {
    scope.onApplicationClick(1);
    loadSavedApplication.resolve();
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(false);
  });

  it('loadSavedApplication reject causes error', function() {
    scope.onApplicationClick(1);
    loadSavedApplication.reject({data: 'error'});
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(true);
    expect(scope.applicationLoadError.message).toBe('error');  
  });

  it('loads new application with Submitted status name', function() {
    scope.onApplicationClick(2);
    downloadApplicationPdf.resolve();
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(false);
  });

  it('downloadApplicationPdf reject causes error', function() {
    scope.onApplicationClick(2);
    downloadApplicationPdf.reject({data: 'error'});
    scope.$apply();
    expect(scope.applicationLoadError.status).toBe(true);
  });

});
