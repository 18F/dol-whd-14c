describe('anchorLink', function() {
  beforeEach(module('14c'));

  var element, rootScope, anchorScrollMock, documentMock;

  beforeEach(function() {
    element = angular.element('<anchor-link anchor="scroll-target"/>');
    inject(function($rootScope, $anchorScroll, $document, $compile) {
      rootScope = $rootScope;
      anchorScrollMock = $anchorScroll;
      documentMock = $document;
      $compile(element)(rootScope);
    });
  });

  it('invoke directive', function() {
    rootScope.$digest();
    expect(element).toBeDefined();
  });

  it('focuses the target element on click', function() {
    rootScope.$digest();
    var focus = jasmine.createSpy();
    spyOn(documentMock[0], 'getElementById').and.returnValue({ focus: focus });
    element.trigger('click');

    expect(documentMock[0].getElementById).toHaveBeenCalled();
    expect(focus).toHaveBeenCalled();
  });

  describe('focuses the target element on enter key', function() {
    it('keypress', function() {
      rootScope.$digest();
      var focus = jasmine.createSpy();
      spyOn(documentMock[0], 'getElementById').and.returnValue({ focus: focus });
      element.triggerHandler({ type: 'keypress', which: 13 });

      expect(documentMock[0].getElementById).toHaveBeenCalled();
      expect(focus).toHaveBeenCalled();
    });

    it('keydown', function() {
      rootScope.$digest();
      var focus = jasmine.createSpy();
      spyOn(documentMock[0], 'getElementById').and.returnValue({ focus: focus });
      element.triggerHandler({ type: 'keydown', which: 13 });

      expect(documentMock[0].getElementById).toHaveBeenCalled();
      expect(focus).toHaveBeenCalled();
    });
  });

  describe('does not focus the target element on other key', function() {
    it('keypress', function() {
      rootScope.$digest();
      var focus = jasmine.createSpy();
      spyOn(documentMock[0], 'getElementById').and.returnValue({ focus: focus });
      element.triggerHandler({ type: 'keypress', which: 14 });

      expect(documentMock[0].getElementById).not.toHaveBeenCalled();
      expect(focus).not.toHaveBeenCalled();
    });

    it('keydown', function() {
      rootScope.$digest();
      var focus = jasmine.createSpy();
      spyOn(documentMock[0], 'getElementById').and.returnValue({ focus: focus });
      element.triggerHandler({ type: 'keydown', which: 14 });

      expect(documentMock[0].getElementById).not.toHaveBeenCalled();
      expect(focus).not.toHaveBeenCalled();
    });
  });
});
