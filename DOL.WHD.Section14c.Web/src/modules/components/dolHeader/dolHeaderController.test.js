describe('dolHeaderController', function() {
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

      it('skip to main content', function() {
        var controller = dolHeaderController();
        controller.skipToMainContent();
        scope.$apply();

        expect(controller.skipToMainContent).toHaveBeenCalled();
      });
    })
  );

});
