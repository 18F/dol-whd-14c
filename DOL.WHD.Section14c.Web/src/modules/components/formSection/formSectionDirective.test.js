describe('formSection', function() {

    beforeEach(module('14c'));

    var element, rootScope;
    beforeEach(function () {
        element = angular.element('<form-section/>');
    	inject(function ($rootScope, $compile) {
            rootScope = $rootScope;
			//$compile(element)(rootScope);
			rootScope.$digest();
        });
    });

    it('invoke directive', function() {
        // compiled in init.
    });
});

describe('helplink', function() {

    beforeEach(module('14c'));

    var element, rootScope;
    beforeEach(function () {
        element = angular.element('<helplink/>');
    	inject(function ($rootScope, $compile) {
            rootScope = $rootScope;
			$compile(element)(rootScope);
			rootScope.$digest();
        });
    });

    it('invoke directive', function() {
        // compiled in init.
    });
});

describe('helptext', function() {

    beforeEach(module('14c'));

    var element, rootScope;
    beforeEach(function () {
        element = angular.element('<helptext/>');
    	inject(function ($rootScope, $compile) {
            rootScope = $rootScope;
			$compile(element)(rootScope);
			rootScope.$digest();
        });
    });

    it('invoke directive', function() {
        // compiled in init.
    });
});