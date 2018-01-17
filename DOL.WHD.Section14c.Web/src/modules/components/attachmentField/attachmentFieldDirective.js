'use strict';

module.exports = function(ngModule) {
  ngModule.directive('attachmentField', function() {
    'use strict';

    return {
      restrict: 'E',
      template: require('./attachmentFieldTemplate.html'),
      controller: 'attachmentFieldController',
      scope: {
        attachments:"=",
        modelPrefix: '@',
        inputId: '@'
      },
      controllerAs: 'vm'
    };
  });
};
