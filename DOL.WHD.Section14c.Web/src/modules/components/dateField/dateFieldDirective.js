'use strict';

module.exports = function(ngModule) {
  ngModule.directive('dateField', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./dateFieldTemplate.html'),
      controller: 'dateFieldController',
      scope: {
        dateVal: '='
      },
      controllerAs: 'vm'
    };
  });
};
