'use strict';

module.exports = function(ngModule) {
  ngModule.directive('anchorLink', function($anchorScroll, $document) {
    'use strict';

    return {
      template: '<a href scrollTo={{anchor}} class="dol-skip-to-main">skip to main content</a>',
      replace: false,
      link: function(scope, element, attr) {
        element.bind('click', function() {
          $anchorScroll(attr.scrollto);
          $document[0].getElementById(attr.scrollto).focus();
        });
      }
    };
  });
};
