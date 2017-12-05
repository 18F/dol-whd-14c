describe('adminDashboardController', function() {
  var scope, q, locationMock, responsesServiceMock, stateServiceMock, statusesServiceMock;
  var adminDashboardController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $q, $location, responsesService, stateService, statusesService) {
      scope = $rootScope.$new();
      q = $q;
      locationMock = $location;
      responsesServiceMock = responsesService;
      stateServiceMock = stateService;
      statusesServiceMock = statusesService;

      adminDashboardController = function() {
        return $controller('adminDashboardController', {
          $scope: scope,
          $location: locationMock,
          responsesService: responsesServiceMock,
          stateService: stateServiceMock,
          statusesService: statusesServiceMock
        });
      };
    })
  );

  describe('initialization/constructor', function() {
    it('initializes with statuses from the status service', function() {
      var deferred = q.defer();
      spyOn(statusesServiceMock, 'getStatuses').and.returnValue(deferred.promise);
      adminDashboardController();

      expect(statusesServiceMock.getStatuses).toHaveBeenCalled();

      deferred.resolve('status object');
      scope.$digest();

      expect(scope.filterStatuses).toBe('status object');
    });

    it('initializes with responses from the responses service', function() {
      var deferred = q.defer();
      spyOn(responsesServiceMock, 'getQuestionResponses').and.returnValue(deferred.promise);
      adminDashboardController();

      expect(responsesServiceMock.getQuestionResponses).toHaveBeenCalled();

      deferred.resolve({ EstablishmentType: 'response object' });
      scope.$digest();

      expect(scope.establishmentTypes).toBe('response object');
    });
  });

  describe('tests that need a controller with location mocking', function() {
    beforeEach(function() {
      spyOn(locationMock, 'path');
      adminDashboardController();
    })

    describe('gotoApplication', function() {
      it('sets the admin path with the given application ID', function() {
        scope.gotoApplication('application-id');
        expect(locationMock.path).toHaveBeenCalledWith('/admin/application-id');
      });
    });

    describe('gotoUsers', function() {
      it('sets the admin path to the users section', function() {
        scope.gotoUsers();
        expect(locationMock.path).toHaveBeenCalledWith('/admin/users');
      });
    });
  });

  describe('gridOptions customFilters tests', function() {
    beforeEach(function() {
      adminDashboardController();
    });

    describe('filterType', function() {
      var filterType;

      var items = [
        { a: true, b: false, c: [{ id: 1 }] },
        { a: true, b: false, c: [{ id: 2 }] },
        { a: true, b: false, c: [{ id: 3 }] }
      ];

      beforeEach(function() {
        filterType = scope.gridOptions.customFilters.filterType;
      });

      it('returns all items when "value" is falsey', function() {
        var result = filterType(items, false, 'a');
        expect(result).toEqual(items);
      });

      it('returns all items when "predicate" is falsey on all items', function() {
        var result = filterType(items, false, 'b');
        expect(result).toEqual(items);
      });

      it('returns only matching items when "value" is given and some items match the "predicate"', function() {
        var result = filterType(items, 3, 'c');
        expect(result).toEqual([{ a: true, b: false, c: [{ id: 3 }]}]);
      });
    });

    describe('filterStatus', function() {
      var filterStatus;

      var items = [
        { a: 1 },
        { a: 2 },
        { a: 3 }
      ];

      beforeEach(function() {
        filterStatus = scope.gridOptions.customFilters.filterStatus;
      });

      it('returns everything if "value" is falsey', function() {
        var result = filterStatus(items, false, 'a');
        expect(result).toEqual(items);
      });

      it('returns nothing if "predicate" is falsey for every item', function() {
        var result = filterStatus(items, 3, 'b');
        expect(result).toEqual([]);
      });

      it('returns only matching items when "value" is set and some values match the "predicate"', function() {
        var result = filterStatus(items, 3, 'a');
        expect(result).toEqual([{ a: 3 }]);
      });
    });
  });
});
