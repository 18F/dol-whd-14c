'use strict';

module.exports = function(ngModule) {
    ngModule.directive('answerField', function() {
        'use strict'

        return {
            transclude: true,
            template: require('./answerFieldTemplate.html'),
            replace: true,
            scope: {
                answer: '='
            }
        }
    });
}
