module.exports = function($scope, dataService) {
    'use strict';

    dataService.GetNumbers().then(function (data) {
        $scope.numbers = data;
    });

};