'use strict';

module.exports = function(ngModule) {
  ngModule.directive('attachmentField', function() {

      'use strict';

      return {
          restrict: 'EA',
          template: require('./attachmentFieldTemplate.html'),
          controller: 'attachmentFieldController',
          scope: {
              attachmentId: '=',
              attachmentName: '='
          },
          controllerAs: 'vm'
      };
  });
}
