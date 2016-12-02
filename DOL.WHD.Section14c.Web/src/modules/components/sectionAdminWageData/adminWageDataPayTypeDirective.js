'use strict';

module.exports = function(ngModule) {
  ngModule.directive('adminWageDataPayType', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./adminWageDataPayTypeTemplate.html'),
          scope: {
              paytype: '=',
              data: '='
          }
      };
  });
}