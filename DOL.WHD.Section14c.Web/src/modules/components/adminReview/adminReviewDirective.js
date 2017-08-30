'use strict';

module.exports = function(ngModule) {
  ngModule.directive('adminReview', function() {
    'use strict';

    return {
      restrict: 'E',
      template: require('./adminReviewTemplate.html'),
      controller: 'adminReviewController',
      scope: {
        appid: '@'
      },
      transclude: true,
      link: function(scope, element, attrs, ctrlr, transclude) {
        transclude(scope, function(clone, scope) {
          angular
            .element(element[0].querySelector('.admin-content'))
            .append(clone);
        });
      }
    };
  });
};
