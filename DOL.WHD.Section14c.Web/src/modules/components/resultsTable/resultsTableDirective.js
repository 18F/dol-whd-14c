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
        scope.$watch('results', function(newValue) {
          console.log(scope)
          scope.vm.refreshTable(newValue, scope.columns);
        }, true);
      },
      controllerAs: 'vm'
    };
  });
};
