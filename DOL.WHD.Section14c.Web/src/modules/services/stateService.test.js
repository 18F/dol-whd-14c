describe('stateService', function() {
    beforeEach(module('14c'));

    var stateService;

    beforeEach(inject(function($injector) {
        stateService = $injector.get('stateService');
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

    it('should set the access_token property', function() {
        stateService.access_token = 'value';
        expect(stateService.access_token).toEqual('value');
    });
    
    it('should set the formData property', function() {
        stateService.formData = 'value';
        expect(stateService.formData).toEqual('value');
    });

    it('should clear the application state on logout', function() {
        stateService.logOut();
        expect(stateService.activeEIN).toEqual(undefined);
    });
});
