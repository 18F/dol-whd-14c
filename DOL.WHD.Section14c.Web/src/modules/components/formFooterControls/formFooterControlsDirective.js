'use strict';

module.exports = function(ngModule) {
  ngModule.directive('formFooterControls', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./formFooterControlsTemplate.html'),
          controller: 'formFooterControlsController',
          scope: {},
          controllerAs: 'vm'
      };
  });
}
