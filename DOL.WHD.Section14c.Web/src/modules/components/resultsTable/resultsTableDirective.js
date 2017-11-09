module.exports = function(ngModule) {
  ngModule.directive('resultsTable', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./resultsTableTemplate.html'),
      controller: 'resultsTableController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
