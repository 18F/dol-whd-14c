'use strict';
// This attribute directive gives focus to its parent element based on an expression.
// If that expression evaluates to a falsey value, the element is blurred.
// If the same expression evaluates to a truthy value, the element is focused
module.exports = function(ngModule) {
  ngModule.directive('focusOn', function($timeout) {
    'use strict';

    return {
      restrict : 'A',
        link : function($scope,$element,$attr) {
            $scope.$watch($attr.focusOn,function(_focusVal) {
                $timeout(function() {
                    _focusVal ? $element[0].focus() :
                        $element[0].blur();
                });
            });
        }
    };
  });
}
