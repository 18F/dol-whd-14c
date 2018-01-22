'use strict';

module.exports = function(ngModule) {
  ngModule.directive('attachmentsField', function() {
    'use strict';

    return {
      restrict: 'E',
      template: require('./attachmentsFieldTemplate.html'),
      controller: 'attachmentsFieldController',
      scope: {
        allowMultiUpload: '=',
        attachments:"=",
        modelPrefix: '@',
        inputId: '@'
      },
      controllerAs: 'vm'
    };
  });
};
