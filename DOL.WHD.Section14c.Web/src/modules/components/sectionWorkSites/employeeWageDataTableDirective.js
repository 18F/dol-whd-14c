module.exports = function(ngModule) {
  ngModule.directive('employeeWageDataTable', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./employeeWageDataTableTemplate.html'),
      controller: 'sectionWorkSitesController',
      scope: {},
      controllerAs: 'vm'
    };
  });
};
