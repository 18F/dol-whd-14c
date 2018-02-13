describe('panelService', function() {
  var panelService;
  var $ = jasmine.createSpy();
  var $methods = {
    on: jasmine.createSpy(),
    keydown: jasmine.createSpy(),
    attr: jasmine.createSpy(),
    addClass: jasmine.createSpy(),
    focus: jasmine.createSpy(),
    removeClass: jasmine.createSpy(),
    hasClass: jasmine.createSpy()
  };

  beforeEach(module('14c'));

  beforeEach(function() {
    $.calls.reset();
    $methods.on.calls.reset();
    $methods.keydown.calls.reset();
    $methods.attr.calls.reset();
    $methods.addClass.calls.reset();
    $methods.focus.calls.reset();
    $methods.removeClass.calls.reset();
    $methods.hasClass.calls.reset();
    $.and.returnValue($methods);

    module('14c');
    inject(function($injector) {
      panelService = $injector.get('panelService');
    });
  });

  it('sets up events on the expected DOM elements', function() {
    var testDoc = { };
    const handler = panelService.setup($, testDoc, '.my-test-dom');

    expect($).toHaveBeenCalledWith('.my-test-dom');
    expect($).toHaveBeenCalledWith('.cd-panel-close');
    expect($).toHaveBeenCalledWith('.cd-panel .dol-last-focus');
    expect($).toHaveBeenCalledWith('.cd-panel .dol-first-focus');
    expect($).toHaveBeenCalledWith(testDoc);

    expect($methods.on).toHaveBeenCalledWith('click', handler.showThePanel);
    expect($methods.on).toHaveBeenCalledWith('click', handler.panelCloseHandler);

    expect($methods.keydown).toHaveBeenCalledWith(handler.globalKeydownHandler);
    expect($methods.keydown).toHaveBeenCalledWith(handler.trapKeyboardOnLastFocus);
    expect($methods.keydown).toHaveBeenCalledWith(handler.trapKeyboardOnFirstFocus);
  });

  it('shows the panel', function() {
    var e = {
      preventDefault: jasmine.createSpy()
    };
    $methods.attr.and.returnValue('attr-value');

    const handler = panelService.setup($);

    handler.showThePanel(e);

    expect($).toHaveBeenCalledWith(handler);
    expect($methods.attr).toHaveBeenCalledWith('aria-controls');
    expect($).toHaveBeenCalledWith('#attr-value');
    expect($methods.addClass).toHaveBeenCalledWith('is-visible');
    expect($).toHaveBeenCalledWith('#attr-value .cd-panel-header h3');
    expect($methods.focus.calls.count()).toBe(1);
    expect($).toHaveBeenCalledWith('body');
    expect($methods.addClass).toHaveBeenCalledWith('cd-panel-open');
    expect(e.preventDefault.calls.count()).toBe(1);
  });

  it('hides the panel', function() {
    const handler = panelService.setup($);
    handler.showThePanel({ preventDefault: function() { }});
    handler.closeSlidingPanel();

    expect($).toHaveBeenCalledWith('.cd-panel');
    expect($methods.removeClass).toHaveBeenCalledWith('is-visible');
    expect($).toHaveBeenCalledWith('body');
    expect($methods.removeClass).toHaveBeenCalledWith('cd-panel-open');
  });

  describe('sets up a global keydown handler...', function() {
    it('does not do anything if the panel is not visible', function() {
      $methods.hasClass.and.returnValue(false);
      var e = {
        keyCode: 27
      };
      const handler = panelService.setup($);
      spyOn(handler, 'closeSlidingPanel');

      handler.globalKeydownHandler(e);

      expect($).toHaveBeenCalledWith('.cd-panel');
      expect($methods.hasClass).toHaveBeenCalledWith('is-visible');
      expect(handler.closeSlidingPanel).not.toHaveBeenCalled();
    });

    it('does not do anything if the keypress is not ESC', function() {
      $methods.hasClass.and.returnValue(true);
      var e = {
        keyCode: 900
      };
      const handler = panelService.setup($);
      spyOn(handler, 'closeSlidingPanel');

      handler.globalKeydownHandler(e);

      expect($).toHaveBeenCalledWith('.cd-panel');
      expect($methods.hasClass).toHaveBeenCalledWith('is-visible');
      expect(handler.closeSlidingPanel).not.toHaveBeenCalled();
    });

    it('hides the panel if it is visible and the ESC key is pressed', function() {
      $methods.hasClass.and.returnValue(true);
      var e = {
        keyCode: 27
      };
      const handler = panelService.setup($);
      spyOn(handler, 'closeSlidingPanel');
      handler.showThePanel({ preventDefault: function() {} });

      handler.globalKeydownHandler(e);

      expect($).toHaveBeenCalledWith('.cd-panel');
      expect($methods.hasClass).toHaveBeenCalledWith('is-visible');

      // this check serves as a proxy that the closeSlidingPanel method
      // was called; due to scoping weirdness, we can't actually check
      // the spy...
      expect($methods.removeClass).toHaveBeenCalledWith('cd-panel-open');
    });
  });

  it('closes the panel when the close button is clicked', function() {
    const e = {
      preventDefault: jasmine.createSpy()
    };
    const handler = panelService.setup($);
    handler.showThePanel(e);
    e.preventDefault.calls.reset();

    handler.panelCloseHandler(e);

    // proxy for checking closeSlidingPanel method
    expect($methods.removeClass).toHaveBeenCalledWith('cd-panel-open');
    expect(e.preventDefault.calls.count()).toBe(1);
  });

  describe('keeps focus inside the panel...', function() {
    describe('when the user tabs from the last focusable element in the panel...', function() {
      it('allows the default behavior if the keypress is not tab', function() {
        const e = {
          preventDefault: jasmine.createSpy(),
          which: 27,
          shiftKey: false
        };
        const handler = panelService.setup($);
        $.calls.reset();

        handler.trapKeyboardOnLastFocus(e);

        expect($.calls.count()).toBe(0);
        expect(e.preventDefault.calls.count()).toBe(0);
      });

      it('allows the default behavior if the shift key is down', function() {
        const e = {
          preventDefault: jasmine.createSpy(),
          which: 9,
          shiftKey: true
        };
        const handler = panelService.setup($);
        $.calls.reset();

        handler.trapKeyboardOnLastFocus(e);

        expect($.calls.count()).toBe(0);
        expect(e.preventDefault.calls.count()).toBe(0);
      });

      it('moves focus back to the first focusable element if tab is pressed and shift key is not down', function() {
        const e = {
          preventDefault: jasmine.createSpy(),
          which: 9,
          shiftKey: false
        };
        const handler = panelService.setup($);
        $.calls.reset();

        handler.trapKeyboardOnLastFocus(e);

        expect($).toHaveBeenCalledWith('.cd-panel .dol-first-focus');
        expect($methods.focus.calls.count()).toBe(1);
        expect(e.preventDefault.calls.count()).toBe(1);
      });
    });

    describe('when the user shift-tabs from the first focusable element in the panel...', function() {
      it('allows the default behavior if the keypress is not tab', function() {
        const e = {
          preventDefault: jasmine.createSpy(),
          which: 27,
          shiftKey: true
        };
        const handler = panelService.setup($);
        $.calls.reset();

        handler.trapKeyboardOnFirstFocus(e);

        expect($.calls.count()).toBe(0);
        expect(e.preventDefault.calls.count()).toBe(0);
      });

      it('allows the default behavior if the shift key is not down', function() {
        const e = {
          preventDefault: jasmine.createSpy(),
          which: 9,
          shiftKey: false
        };
        const handler = panelService.setup($);
        $.calls.reset();

        handler.trapKeyboardOnFirstFocus(e);

        expect($.calls.count()).toBe(0);
        expect(e.preventDefault.calls.count()).toBe(0);
      });

      it('moves focus back to the last focusable element if tab is pressed and shift key is not down', function() {
        const e = {
          preventDefault: jasmine.createSpy(),
          which: 9,
          shiftKey: true
        };
        const handler = panelService.setup($);
        $.calls.reset();

        handler.trapKeyboardOnFirstFocus(e);

        expect($).toHaveBeenCalledWith('.cd-panel .dol-last-focus');
        expect($methods.focus.calls.count()).toBe(1);
        expect(e.preventDefault.calls.count()).toBe(1);
      });
    });
  });
});
