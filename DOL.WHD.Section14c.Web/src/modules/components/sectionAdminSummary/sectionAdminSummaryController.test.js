describe('sectionWageDataController', function() {
  var q, scope, cookiesMock, stateServiceMock, statusesServiceMock, apiServiceMock;
  var sectionAdminSummaryController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, $q, $cookies, stateService, statusesService, apiService) {
      q = $q;
      scope = $rootScope.$new();
      cookiesMock = $cookies;
      stateServiceMock = stateService;
      statusesServiceMock = statusesService;
      apiServiceMock = apiService;

      sectionAdminSummaryController = function() {
        return $controller('sectionAdminSummaryController', {
          $scope: scope,
          stateService: stateServiceMock,
          statusesService: statusesServiceMock,
          apiService: apiServiceMock
        });
      };
    })
  );

  it('constructor initializes with data from statuses service', function() {
    var deferred = q.defer();
    spyOn(statusesServiceMock, 'getStatuses').and.returnValue(deferred.promise);
    var controller = sectionAdminSummaryController();

    expect(statusesServiceMock.getStatuses).toHaveBeenCalled();

    deferred.resolve('statuses object');
    scope.$digest();

    expect(controller.statuses).toBe('statuses object');
  });

  it('updateStatus calls API service', function() {
    spyOn(apiServiceMock, 'changeApplicationStatus');
    spyOn(cookiesMock, 'get').and.returnValue('an access token');
    var controller = sectionAdminSummaryController();
    scope.appid = 'the-app-id'
    scope.appData = { statusId: 'app-data-status-id' };

    controller.updateStatus();

    expect(apiServiceMock.changeApplicationStatus).toHaveBeenCalledWith('an access token', 'the-app-id', 'app-data-status-id');
  })
});
