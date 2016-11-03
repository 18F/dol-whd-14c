'use strict';
import * as _ from 'lodash';

module.exports = function(ngModule) {
    ngModule.controller('dateFieldController', function($scope, moment) {
        'ngInject';
        'use strict';

        var vm = this;

        function updateLocalScope() {
            if(moment.isDate($scope.dateVal)) {
                const dateValMoment = moment($scope.dateVal);
                // parse out values
                vm.month = dateValMoment.month() + 1; // month is zero-based
                vm.day = dateValMoment.date();
                vm.year = dateValMoment.year();
            }
        }

        $scope.$watch('dateVal', function(newValue, oldValue) {
            if(moment.isDate(newValue) && !moment(newValue).isSame(oldValue)) {
                updateLocalScope();
            }
        });

        $scope.$watchGroup(['vm.year', 'vm.month', 'vm.day'], function(newValues, oldValues) {
            if(_.difference(newValues, oldValues).length > 0 && vm.year && vm.month && vm.day) {
                $scope.dateVal = new Date(vm.year, vm. month - 1, vm.day);
            }
        });

        updateLocalScope();
    });
}