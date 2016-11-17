describe('stateService', function() {
    beforeEach(module('14c'));

    var stateService;

    beforeEach(inject(function(_$rootScope_, _$q_, _stateService_, _apiService_) {
        stateService = _stateService_;
        apiService = _apiService_;
        $q = _$q_;
        getApplication = $q.defer();
        $scope = _$rootScope_.$new();
        spyOn(apiService, 'getApplication').and.returnValue(getApplication.promise);
    }));

    it('should set form data', function() {
        stateService.setFormData({testProperty: 1234});
        expect(stateService.formData.testProperty).toEqual(1234);
    });

    it('should set form value', function() {
        stateService.setFormValue('testProperty', 'testValue');
        expect(stateService.formData.testProperty).toEqual('testValue');
    });

    it('should set the user property', function() {
        stateService.user = 'value';
        expect(stateService.user).toEqual('value');
    });

    it('should set the ein property', function() {
        stateService.ein = 'value';
        expect(stateService.ein).toEqual('value');
    });
    
    it('should set the formData property', function() {
        stateService.formData = 'value';
        expect(stateService.formData).toEqual('value');
    });

    it('should clear the application state on logout', function() {
        stateService.logOut();
        expect(stateService.activeEIN).toEqual(undefined);
    });

    it('should return if the user has a claim', function() {
        hasClaim = stateService.hasClaim();
        expect(hasClaim).toEqual(false);
    });

    it('should return if the user has a claim', function() {
        var claimName = 'test';
        stateService.user = {applicationClaims: ['DOL.WHD.Section14c.' + claimName]};
        hasClaim = stateService.hasClaim(claimName);
        expect(hasClaim).toEqual(true);
    });   

    it('should load user info and application data', function() {
        stateService.loadSavedApplication();
        getApplication.resolve({data: '{}'});
        $scope.$digest();

        //TODO: Add asseritions
    });     

    it('should load user info and fail on application data', function() {
        stateService.loadSavedApplication();
        getApplication.reject();
        $scope.$digest();

        //TODO: Add asseritions
    });               
    
});
