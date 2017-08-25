describe('formFooterControls', function() {

    beforeEach(module('14c'));

    var element, rootScope;
    beforeEach(function () {
        element = angular.element('<form-footer-controls/>');
    	inject(function ($rootScope, $compile) {
            rootScope = $rootScope;
			//$compile(element)(rootScope);
        });
    });

    it('invoke directive', function() {
        rootScope.$digest();
        expect(element).toBeDefined();
    });
});