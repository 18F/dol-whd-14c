'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionReview', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./sectionReviewTemplate.html'),
          controller: 'sectionReviewController',
          scope: { },
          controllerAs: 'vm'
      };
  });

  ngModule.directive('reviewbar', function() {
      'use strict'

      return {
          template: '<div class="reviewbar {{ errorstate ? \'error\' : \'\'}}"><div class="reviewbar-container"><div class="reviewbar-bar"></div><div class="reviewbar-circle"></div></div></div>',
          replace: true,
          scope: {
              errorstate: '@'
          }
      }
  });
}
