module.exports = function(ngModule) {
  ngModule.directive('resultsTable', function() {
    'use strict';
    return {
      restrict: 'EA',
      template: require('./resultsTableTemplate.html'),
      controller: 'resultsTableController',
      scope: {
        results: "=results",
        columns: "=columns",
        tableId: "@"
      },
      link: function(scope, controller) {
        scope.$watch('results', function(newValue, oldValue) {
          scope.vm.refreshTable(newValue, scope.columns);
        }, true);
      },
      controllerAs: 'vm'
    };
  });
};
