'use strict';

module.exports = function(ngModule) {
    ngModule.controller('stateFieldController', function($scope) {
        'ngInject';
        'use strict';
        
        var vm = this;

        vm.stateVal = $scope.selectedState;
    
        $scope.$watch('vm.stateVal', function(newValue, oldValue) {
             $scope.selectedState = newValue;
        });

    });

}