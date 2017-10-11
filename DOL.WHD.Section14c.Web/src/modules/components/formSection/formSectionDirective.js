'use strict';

module.exports = function(ngModule) {
  ngModule.directive('formSection', function() {
    'use strict';

    return {
      restrict: 'EA',
      transclude: true,
      template: require('./formSectionTemplate.html'),
      scope: {}
    };
  });

  ngModule.directive('helplink', function() {
    'use strict';

    return {
      template: '<button type="button" class="dol-help-link">?</button>',
      replace: true,
      link: function(scope, element, attrs) {
        element.bind('click', function() {
          element.next().toggleClass('show');
        });
      }
    };
  });

  ngModule.directive('helptext', function() {
    'use strict';

    return {
      transclude: true,
      template: `
        <div id="{{ id }}" class="dol-help-text">
          <ng-transclude></ng-transclude>
        </div>
      `,
      replace: true,
      link: function(scope, element, attrs) {
        scope.$watch('showAllHelp', function() {
          element.toggleClass('show', scope.showAllHelp === true);
        });
      }
    };
  });
};
