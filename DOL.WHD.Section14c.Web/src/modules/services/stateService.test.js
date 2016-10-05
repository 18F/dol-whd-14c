describe('stateService', function() {
    beforeEach(module('14c'));

    var stateService;

    beforeEach(inject(function($injector) {
        stateService = $injector.get('stateService');
    }));

    it('should set form data', function() {
        stateService.setFormData({testProperty: 1234});
        expect(stateService.form_data.testProperty).toEqual(1234);
    });

    it('should set form value', function() {
        stateService.setFormValue('testProperty', 'testValue');
        expect(stateService.form_data.testProperty).toEqual('testValue');
    });
});
