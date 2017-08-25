describe('mainHeaderControl', function() {

    beforeEach(module('14c'));

    var element, rootScope;
    beforeEach(function () {
        element = angular.element('<main-header-control/>');
    	inject(function ($rootScope, $compile, $route) {
            rootScope = $rootScope;
			//$compile(element)(rootScope);
        });
    });

    it('invoke directive', function() {
        rootScope.$digest();
        expect(element).toBeDefined();
    });
});