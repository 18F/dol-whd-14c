'use strict';

module.exports = function(ngModule) {
    ngModule.directive('formSection', function() {

        'use strict';

        return {
            restrict: 'EA',
            transclude: true,
            template: require('./formSectionTemplate.html'),
            scope: { }
        };
    });

    ngModule.directive('helplink', function() {
        'use strict'

        return {
            template: '<div class="help-link">?</div>',
            replace: true,
            link: function(scope, element, attrs) {
                element.bind('click', function() {
                    console.log("clicked");
                    element.next().toggleClass('show');
                })
            }
        }
    });

    ngModule.directive('helptext', function() {
        'use strict'

        return {
            transclude: true,
            template: '<div class="help-text"><ng-transclude></ng-transclude></div>',
            replace: true,
            link: function(scope, element, attrs) {
                scope.$watch('showAllHelp', function() {
                    element.toggleClass('show', scope.showAllHelp === true);
                });
            }
        }
    });
}
