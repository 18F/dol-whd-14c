module.exports = function(ngModule) {
  ngModule.directive('resultsTable', function() {
    'use strict';
    return {
      restrict: 'EA',
      template: require('./resultsTableTemplate.html'),
      controller: 'resultsTableController',
      scope: {
        "results": "=results",
        "columns": "=columns",
        "definitions": "=definitions"
      },
      link: function(scope) {
        scope.vm.initDatatable();
        console.log(scope.results, scope.columns, scope.definitions);
        scope.$watch('results', function(newValue) {
          scope.vm.refreshTable(newValue, scope.columns);
        }, true);
      },
      controllerAs: 'vm'
    };
  });
};
