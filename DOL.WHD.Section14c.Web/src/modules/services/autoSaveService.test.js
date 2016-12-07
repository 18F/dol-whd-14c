describe('autoSaveService', function() {

    beforeEach(module('14c'));

    var autoSave;
    var api;
    var timer;
    var duration = 60 * 1000;// 60 seconds
    var $timeout;
    var $q;
    var deferred;
    var $scope;
    var current;
    beforeEach(inject(function($injector, _$timeout_, _$q_, _$rootScope_, _autoSaveService_, _apiService_, _stateService_, _$cookies_) {
        autoSave = _autoSaveService_;
        api = _apiService_;
        $timeout = _$timeout_;
        $q = _$q_;
        stateService = _stateService_;
        $cookies = _$cookies_;
        deferred = $q.defer();
        $scope = _$rootScope_.$new();
        spyOn(api, 'saveApplication').and.returnValue(deferred.promise);


    }));

    it('should call the start method', function() {
        autoSave.start();
        //expect(timer).toNotBe(undefined);
    });

    it('should call the stop method', function() {
        autoSave.stop;
        //expect(autoSave.stop).toEqual('value');
    });

    it('should call the save method, succeed and call the callback', function() {
        var hasRun = false;
        var callback = function() {
            hasRun=true;
        };
        autoSave.save(callback);
        deferred.resolve();
        $scope.$digest();
        expect(hasRun).toBe(true);
    });

    it('should call the save method, fail and call the callback', function() {
        var hasRun = false;
        var callback = function() {
            hasRun=true;
        };
        autoSave.save(callback);
        deferred.reject();
        $scope.$digest();
        expect(hasRun).toBe(true);
    });    

    it('should call the save method, succeed and call the callback', function() {
        // mock a token cookie
        spyOn($cookies, 'get').and.returnValue('token');
        stateService.ein = '30-1234567';
        var hasRun = false;
        var callback = function() {
            hasRun=true;
        };
        autoSave.save(callback);
        deferred.resolve();
        $scope.$digest();
        expect(hasRun).toBe(true);
    });    

    it('should call the save method, fail and call the callback', function() {
        // mock a token cookie
        spyOn($cookies, 'get').and.returnValue('token');
        stateService.ein = '30-1234567';
        var hasRun = false;
        var callback = function() {
            hasRun=true;
        };
        autoSave.save(callback);
        deferred.reject();
        $scope.$digest();
        expect(hasRun).toBe(true);
    });

    it('should call the nextTimer method', function() {
        autoSave.start();
        $timeout.flush();
    });

});