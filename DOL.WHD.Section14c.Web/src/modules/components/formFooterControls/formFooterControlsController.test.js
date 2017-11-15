describe('formFooterControlsController', function() {
  var scope, $q, mockNavService, mockApiService;
  var mockAutoSaveService, saveApplication;
  var formFooterControlsController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(
      $rootScope,
      $controller,
      _$q_,
      navService,
      apiService,
      autoSaveService
    ) {
      scope = $rootScope.$new();
      $q = _$q_;

      mockNavService = navService;
      mockApiService = apiService;
      mockAutoSaveService = autoSaveService;

      spyOn(mockNavService, 'hasNext');
      spyOn(mockNavService, 'hasBack');
      spyOn(mockNavService, 'getNextSection');

      saveApplication = $q.defer();
      spyOn(mockApiService, 'saveApplication').and.returnValue(
        saveApplication.promise
      );

      formFooterControlsController = function() {
        return $controller('formFooterControlsController', {
          $scope: scope,
          navService: mockNavService
        });
      };
    })
  );

  it('save', function() {
    var controller = formFooterControlsController();
    spyOn(mockAutoSaveService, 'save');
    controller.doSave();
    scope.$apply();

    expect(mockAutoSaveService.save).toHaveBeenCalled();
  });

  it('onNextClick no next', function() {
    var controller = formFooterControlsController();
    spyOn(controller, 'doSave');
    spyOn(mockNavService, 'goNext');
    controller.onNextClick();
    scope.$apply();

    expect(controller.doSave).toHaveBeenCalled();
    expect(mockNavService.goNext).not.toHaveBeenCalled();
  });

  it('onNextClick', function() {
    var controller = formFooterControlsController();
    controller.hasNext = true;
    spyOn(controller, 'doSave');
    spyOn(mockNavService, 'goNext');
    controller.onNextClick();
    scope.$apply();

    expect(controller.doSave).toHaveBeenCalled();
    expect(mockNavService.goNext).toHaveBeenCalled();
  });

  it('onBackClick no back', function() {
    var controller = formFooterControlsController();
    spyOn(controller, 'doSave');
    spyOn(mockNavService, 'goBack');
    controller.onBackClick();
    scope.$apply();

    expect(controller.doSave).toHaveBeenCalled();
    expect(mockNavService.goBack).not.toHaveBeenCalled();
  });

  it('onBackClick', function() {
    var controller = formFooterControlsController();
    controller.hasBack = true;
    spyOn(controller, 'doSave');
    spyOn(mockNavService, 'goBack');
    controller.onBackClick();
    scope.$apply();

    expect(controller.doSave).toHaveBeenCalled();
    expect(mockNavService.goBack).toHaveBeenCalled();
  });

  it('onSaveClick', function() {
    var controller = formFooterControlsController();
    spyOn(controller, 'doSave');
    controller.onSaveClick();
    scope.$apply();

    expect(controller.doSave).toHaveBeenCalled();
  });
});
