module.exports = function(ngModule) {
  ngModule.directive('resultsTable', function() {
    'use strict';
    return {
      restrict: 'E',
      replace: true,
      template: require('./resultsTableTemplate.html'),
      controller: 'resultsTableController',
      scope: {
        results: "=results",
        columns: "=columns"
      },
      link: function(scope) {
        scope.$watch('results', function(newValue, oldValue) {
          console.log('change detected')
          scope.refreshTable(newValue);
        }, true);
      },
      controllerAs: 'vm'
    };
  });
};
