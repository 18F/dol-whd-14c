'use strict';

module.exports = function(ngModule) {
  ngModule.directive('anchorLink', function($anchorScroll, $document) {
    'use strict';

    return {
      template: '<a href role="button" scrollTo={{anchor}} class="dol-skip-to-main">skip to main content</a>',
      replace: false,
      link: function(scope, element, attr) {
        element.bind('click', function() {
          $anchorScroll(attr.scrollto);
          $document[0].getElementById(attr.scrollto).focus();
        });
        element.bind('keydown keypress', function(event) {
          if (event.which === 13) {
            $anchorScroll(attr.scrollto);
            $document[0].getElementById(attr.scrollto).focus();
            event.preventDefault();
          }
        });
      },
      transclude: true
    };
  });
};
