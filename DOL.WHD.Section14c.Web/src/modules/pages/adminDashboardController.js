'use strict';

import findIndex from 'lodash/findIndex'

module.exports = function(ngModule) {
    ngModule.controller('adminDashboardController', function($scope, $location, responsesService, stateService, statusesService) {
        'ngInject';
        'use strict';

        statusesService.getStatuses().then((data) => {
            $scope.filterStatuses = data;
        });

        let questionKeys = [ 'EstablishmentType' ];
        responsesService.getQuestionResponses(questionKeys).then((responses) => {
            $scope.establishmentTypes = responses.EstablishmentType;
        });

        $scope.gridOptions = {
            data: stateService.appList,
            sort: {
                predicate: 'employerName',
                direction: 'asc'
            },
            customFilters: {
                filterType: function (items, value, predicate) {
                    return items.filter(function (item) {
                        return value && item[predicate] ? findIndex(item[predicate], ['id', value]) !== -1 : true;
                    });
                }
            }
        };

        //$scope.gridActions = {};

        $scope.gotoApplication = function(id) {
            $location.path("/admin/" + id);
        }

        $scope.gotoUsers = function() {
            $location.path("/admin/users");
        }
    });
}
