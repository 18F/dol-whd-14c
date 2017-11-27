'use strict';
import * as _ from 'lodash';

module.exports = function(ngModule) {
  ngModule.controller('dateFieldController', function($scope, moment) {
    'ngInject';
    'use strict';

    var vm = this;

    function updateLocalScope() {
      const dateValMoment = moment($scope.dateVal, moment.ISO_8601, true);
      if (dateValMoment.isValid()) {
        // parse out values
        vm.month = dateValMoment.month() + 1; // month is zero-based
        vm.day = dateValMoment.date();
        vm.year = dateValMoment.year();
      }
    }

    $scope.$watch('dateVal', function(newValue) {
      if (!newValue) {
        vm.year = undefined;
        vm.month = undefined;
        vm.day = undefined;
      } else {
        updateLocalScope();
      }
    });

    $scope.$watchGroup(['vm.year', 'vm.month', 'vm.day'], function(
      newValues,
      oldValues
    ) {
      if (
        _.difference(newValues, oldValues).length > 0 &&
        vm.year &&
        vm.month &&
        vm.day
      ) {
        const dateValMoment = moment({
          years: vm.year,
          months: vm.month - 1,
          date: vm.day
        });
        if (dateValMoment.isValid()) {
          $scope.dateVal = dateValMoment.toISOString();
        }
      }
    });

    updateLocalScope();
  });
};
