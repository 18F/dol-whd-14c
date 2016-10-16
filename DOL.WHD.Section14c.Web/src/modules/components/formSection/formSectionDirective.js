'use strict';

module.exports = function(ngModule) {
  ngModule.directive('formSection', function() {

      'use strict';

      return {
          restrict: 'EA',
          transclude: true,
          template: require('./formSectionTemplate.html')
      };
  });
}
