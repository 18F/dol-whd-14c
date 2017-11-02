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

      it('skip to main content link focuses on right div', function() {
        var element = document.getElementById(id);
        spyOn(elememnt, 'foucs');
        var controller = dolHeaderController();
        controller.skipToMainContent('#mainContent');
        scope.$apply();

        expect(controller.skipToMainContent).toHaveBeenCalled();
        expect(element.focus).toHaveBeenCalled();

      });
    })
  );

});
