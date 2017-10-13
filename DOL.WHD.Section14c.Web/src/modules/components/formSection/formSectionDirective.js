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

  ngModule.directive('helplink', function($document) {
    'use strict';

    return {
      template: '<button type="button" role="tooltip" aria-expanded="{{expanded}}" class="dol-help-link">?</button>',
      replace: true,
      link: function(scope, element, attr) {
        element.bind('click', function() {
          console.log(attr.ariaControls);
          scope.expanded = !scope.expanded;
          angular.element($document[0].getElementById(attr.ariaControls)).removeClass('ng-hide');
          angular.element($document[0].getElementById(attr.ariaControls)).toggleClass('show');
        });
        scope.$watch('showAllHelp', function() {         
          scope.expanded = scope.showAllHelp;
        });

      }
    };
  });

  ngModule.directive('helptext', function() {
    'use strict';

    return {
      transclude: true,
      template: `
        <div ng-show="showAllHelp" class="dol-help-text">
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
