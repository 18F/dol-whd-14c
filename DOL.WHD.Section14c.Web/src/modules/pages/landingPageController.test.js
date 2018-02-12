describe('landingPageController', function() {
  var scope, landingPageController, mockAutoSaveService, mockLocation, organizations, mockApiService, mockStateService, $q, location;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, apiService, autoSaveService, $location, stateService, _$q_, $location) {
      scope = $rootScope.$new();
      mockStateService = stateService;
      mockLocation = $location;
      mockAutoSaveService = autoSaveService;
      mockApiService = apiService;
      $q = _$q_;
      location = $location

      landingPageController = function() {
        return $controller('landingPageController', {
          $scope: scope,
          stateService: mockStateService,
          $location: mockLocation,
          autoSaveService: mockAutoSaveService,
          apiService: mockApiService
        });
      };

      controller = landingPageController();
      scope.orgMemberships = [
        {
          employer: {
            id: '12313',
            legalName: 'Test Employer',
            ein: '12-123345',
            physicalAddress: {
              streetAddress: 'Test',
              city:'Mechanicsburg',
              state: 'PA',
              zipCode: '17050',
            }
          },
          createdAt:'',
          lastModifiedAt:'',
          applicationStatus: {
            name: "InProgress"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        },
        {
          employer: {
            id: '1234',
            legalName: 'Test Employer',
            ein: '12-123345',
            physicalAddress: {
              streetAddress: 'Test',
              city:'Mechanicsburg',
              state: 'PA',
              zipCode: '17050',
            }
          },
          createdAt:'',
          lastModifiedAt:'',
          applicationStatus: {
            name: "Submitted"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        },
        {
          employer: {
            id: '3333',
            legalName: 'Test Employer',
            ein: '12-123345',
            physicalAddress: {
              streetAddress: 'Test',
              city:'Mechanicsburg',
              state: 'PA',
              zipCode: '17050',
            }
          },
          createdAt:'',
          lastModifiedAt:'',
          applicationStatus: {
            name: "Invalid"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        }
      ];

      scope.mockSubmittedApplications = [
        {
          employer: {
            id: '1234',
            legalName: 'Test Employer',
            ein: '12-123345',
            physicalAddress: {
              streetAddress: 'Test',
              city:'Mechanicsburg',
              state: 'PA',
              zipCode: '17050',
            }
          },
          action: "Download",
          createdAt:'',
          lastModifiedAt:'',
          applicationStatus: {
            name: "Submitted"
          },
          applicationId: '1231541515',
          ein: "12-123345"
        }
      ];

      saveNewApplication = $q.defer();
      spyOn(mockStateService, 'saveNewApplication').and.returnValue(
        saveNewApplication.promise
      );

      downloadApplicationPdf = $q.defer();
      spyOn(mockStateService, 'downloadApplicationPdf').and.returnValue(
        downloadApplicationPdf.promise
      );

      loadSavedApplication = $q.defer();
      spyOn(mockStateService, 'loadSavedApplication').and.returnValue(
        loadSavedApplication.promise
      );

      spyOn(scope, 'initDatatable');

      userInfo = $q.defer();
      spyOn(mockApiService, 'userInfo').and.returnValue(
        userInfo.promise
      );

      clearApplication = $q.defer();
      spyOn(mockApiService, 'clearApplication').and.returnValue(
        clearApplication.promise
      );

    })
  );

  describe('controller initialization', function(){

    it('loads in progress application', function() {
      scope.init();
      expect(scope.currentApplication).toBe(undefined);
      userInfo.resolve({data:{organizations: scope.orgMemberships}});
      scope.$apply();
      expect(scope.applicationLoadError.status).toBe(false);
      expect(scope.submittedApplications.length).toEqual(1);
      expect(mockStateService.ein).toEqual('12-123345')
      expect(mockStateService.applicationId).toBe(undefined);
      expect(scope.currentApplication.applicationId).toEqual('1231541515');
    });

    it('user info reject does not import applications into submitted list', function() {
      var orgs = scope.submittedApplications;
      scope.submittedApplications = [];
      scope.$digest();
      scope.init();
      userInfo.reject({data:{organizations: orgs}});
      scope.$apply();
      expect(scope.submittedApplications.length).toEqual(0);
    });
  });

  describe('account controls', function(){
    it('has navigation to change password', function() {
      spyOn(mockLocation, 'path');
      scope.changePassword();
      scope.$apply();
      expect(mockLocation.path).toHaveBeenCalledWith("/changePassword");
    });
  });

  describe('clear application', function(){
    it('clear application success', function() {
      spyOn(mockLocation, 'path');
      scope.clearApplication();
      expect(scope.clear.status).toEqual("Clearing");
      expect(scope.clear.message).toEqual("Attempting to clear application.");
      clearApplication.resolve();
      scope.$apply();
      expect(scope.navToApplicationButtonName).toEqual("Start New Application");
      expect(scope.clearApplicationModalIsVisible).toBe(false);
    });

    it('clear application failure', function() {
      spyOn(mockLocation, 'path');
      scope.clearApplication();
      expect(scope.clear.status).toEqual("Clearing");
      expect(scope.clear.message).toEqual("Attempting to clear application.");
      clearApplication.reject();
      scope.$apply();
      expect(scope.clear.status).toEqual("Failure");
      expect(scope.clear.message).toEqual("Failed to clear application.");
    });
  });

  describe('application download', function(){
    it('user cannot download application with action other than Download', function() {
      scope.submittedApplications = scope.mockSubmittedApplications;
      scope.submittedApplications[0].action = "Invalid";
      scope.downloadApplication(0);
      expect(scope.applicationLoadError.status).toEqual(true);
      expect(scope.applicationLoadError.message).toEqual('Invalid Status for Download');
    });

    it('user can download application with Submitted status', function() {
      scope.submittedApplications = scope.mockSubmittedApplications;
      scope.downloadApplication(0);
      downloadApplicationPdf.resolve();
      scope.$apply();
      expect(scope.applicationLoadError.status).toEqual(false);
    });

    it('download pdf rejection causes error', function() {
      scope.submittedApplications = scope.mockSubmittedApplications;
      scope.downloadApplication(0);
      downloadApplicationPdf.reject({data: 'error'})
      scope.$apply();
      expect(scope.applicationLoadError.status).toEqual(true);
      expect(scope.applicationLoadError.message).toEqual('error');
    });
  });

  describe('application navigation', function(){
    it('user can start a new application', function() {
      mockStateService.ein = '1234';
      spyOn(mockAutoSaveService, 'start');
      spyOn(mockLocation, 'path');
      scope.startNewApplication();
      saveNewApplication.resolve({data: {ApplicationId: '1234'}});
      scope.$apply();
      expect(mockAutoSaveService.start).toHaveBeenCalled();
      expect(mockLocation.path).toHaveBeenCalledWith('/section/assurances');
      expect(mockStateService.applicationId).toEqual('1234');
      expect(scope.applicationLoadError.status).toEqual(false);
    });

    it('user can navigate to in progress application', function() {
      mockStateService.ein = '1234';
      spyOn(mockAutoSaveService, 'start');
      spyOn(mockLocation, 'path');
      scope.navToApplication();
      scope.$apply();
      expect(mockAutoSaveService.start).toHaveBeenCalled();
      expect(mockLocation.path).toHaveBeenCalledWith('/section/assurances');
    });
  });

  describe('error handler', function(){
    it('handles error', function() {
      scope.handleApplicationLoadError('test');
      scope.$apply();
      expect(scope.applicationLoadError.status).toEqual(true);
      expect(scope.applicationLoadError.message).toEqual('test');
    });
  });

  describe('data table', function(){
    it('initializes datatable', function() {
      scope.init();
      userInfo.resolve({data:{organizations: scope.orgMemberships}});
      scope.$apply();
      expect(scope.initDatatable).toHaveBeenCalled();
    });
  });

  describe('has a table config...', function() {
    var controller;
    beforeEach(function() {
      controller = landingPageController();
      // spyOn(mockDocument[0], 'getElementById');
    });
    describe('with employee columns definition...', function() {
      it('converts the 4th column to "yes" for truthy values', function() {
        var testFn = scope.tableConfig.applicationColumnDefinitions[3].render;
        var output = testFn("Download");

        expect(output).toEqual("<button>Download</button>")
        expect(scope.tableConfig).toBeDefined();
        expect(scope.tableConfig.applicationColumnDefinitions).toBeDefined();
        expect(scope.tableConfig.applicationColumns).toBeDefined();
        expect(scope.tableConfig.order).toBeDefined();
      });
    });
  })
});
