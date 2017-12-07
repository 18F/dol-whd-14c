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
      link: function(scope, element, attrs) {
        scope.vm.initDatatable(attrs.id);
        scope.$watch('results', function(newValue) {
          scope.vm.refreshTable(newValue, scope.columns, attrs.id);
        }, true);
      },
      controllerAs: 'vm'
    };
  });
};
