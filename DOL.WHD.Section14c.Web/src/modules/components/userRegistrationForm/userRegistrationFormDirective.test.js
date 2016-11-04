describe('userRegistrationForm', function() {

    beforeEach(module('14c'));

    var element, rootScope;
    beforeEach(function () {
        element = angular.element('<user-registration-form/>');
    	inject(function ($rootScope, $compile) {
            rootScope = $rootScope;
			//$compile(element)(rootScope);
			rootScope.$digest();
        });
    });

    it('invoke directive', function() {
        rootScope.$digest();
    });
});