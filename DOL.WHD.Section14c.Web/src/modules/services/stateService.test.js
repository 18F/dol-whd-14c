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
});
