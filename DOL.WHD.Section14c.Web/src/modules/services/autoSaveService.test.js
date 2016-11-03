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

    beforeEach(inject(function($injector, _$timeout_, _$q_, _$rootScope_, _autoSaveService_, _apiService_) {
        autoSave = _autoSaveService_;
        api = _apiService_;
        $timeout = _$timeout_;
        $q = _$q_;
        deferred = $q.defer();
        $scope = _$rootScope_.$new();
    }));

    it('should call the start method', function() {
        spyOn(autoSave, 'nextTimer');
        autoSave.start();
        //expect(timer).toNotBe(undefined);
    });

    it('should call the stop method', function() {
        autoSave.stop();
        //expect(autoSave.stop).toEqual('value');
    });

    it('should call the save method', function() {
        spyOn(api, 'saveApplication').and.returnValue(deferred.promise);
        deferred.resolve();
        var hasRun = false;
        var callback = function() {
            hasRun=true;
        };
        autoSave.save(callback);
        expect(hasRun).toBe(false);

    });

    it('should call the nextTimer method', function() {
        autoSave.nextTimer(current);
        expect(autoSave.nextTimer).toEqual('value');
    });

});