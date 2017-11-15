describe('mainHeaderControlController', function() {
  var scope, mockNavService, mockLocation;
  var mockStateService, mockAutoSaveService;
  var mainHeaderControlController;
  
  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $location,
      $rootScope,
      $controller,
      navService,
      autoSaveService,
      stateService
    ) {
      scope = $rootScope.$new();
      mockNavService = navService;
      mockLocation = $location;
      mockStateService = stateService;
      mockAutoSaveService = {
        save: function(callback) {
          callback();
        }
      };

      spyOn(mockNavService, 'hasNext');
      spyOn(mockNavService, 'hasBack');
      spyOn(mockNavService, 'getNextSection');

      mainHeaderControlController = function() {
        return $controller('mainHeaderControlController', {
          $scope: scope,
          navService: mockNavService,
          $location: mockLocation,
          autoSaveService: mockAutoSaveService,
          stateService: mockStateService
        });
      };
    })
  );
});
