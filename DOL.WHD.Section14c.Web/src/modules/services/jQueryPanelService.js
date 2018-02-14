'use strict';

function Handler($, document, panelTriggerSelector) {
  let panelTrigger;

  this.showThePanel = function(event) {
    panelTrigger = $(this);
    var target = $(this).attr('aria-controls');
    $(`#${target}`).addClass('is-visible');
    $(`#${target} .cd-panel-header h3`).focus();
    $('body').addClass('cd-panel-open');
    event.preventDefault();
  };

  // close the panel
  this.closeSlidingPanel = function() {
    $('.cd-panel').removeClass('is-visible');
    panelTrigger.focus();
    $('body').removeClass('cd-panel-open');
  }
  // And do this for scoping capture. 😬
  var closeSlidingPanel = this.closeSlidingPanel;

  this.globalKeydownHandler = function(event) {
    // escape key
    if ($('.cd-panel').hasClass('is-visible') && event.keyCode === 27) {
      closeSlidingPanel();
    }
  };

  this.panelCloseHandler = function(event) {
    closeSlidingPanel()
    event.preventDefault();
  };

  // trap keyboard access inside the panel
  this.trapKeyboardOnLastFocus = function(event) {
    if (event.which === 9 && !event.shiftKey) {
      $('.cd-panel .dol-first-focus').focus();
      event.preventDefault();
    }
  };
  this.trapKeyboardOnFirstFocus = function(event) {
    if (event.shiftKey && event.which === 9) {
      $('.cd-panel .dol-last-focus').focus();
      event.preventDefault();
    }
  };

  $(panelTriggerSelector).on('click', this.showThePanel);
  $('.cd-panel-close').on('click', this.panelCloseHandler);
  $('.cd-panel .dol-last-focus').keydown(this.trapKeyboardOnLastFocus);
  $('.cd-panel .dol-first-focus').keydown(this.trapKeyboardOnFirstFocus);
  $(document).keydown(this.globalKeydownHandler);
}

module.exports = function(ngModule) {
  ngModule.service('panelService', function() {
    'ngInject';
    'use strict';

    this.setup = function($, document, panelTriggerSelector) {
      return new Handler($, document, panelTriggerSelector);
    };
  });
}
