'use strict';

import * as _ from 'lodash'

module.exports = function(ngModule) {
    ngModule.service('statusesService', function($http, _env, $q) {
        'ngInject';
        'useStrict';

        var statuses;
        var url = `${_env.api_url}/api/status`;

        this.getStatuses = function() {

            let d = $q.defer();

            if(statuses)
            {
                // load cached data
                d.resolve(statuses);
            }
            else
            {
                // get data from server
                $http({
                    method: 'GET',
                    url: url
                }).then(function successCallback (data) {
                    // cache data
                    statuses = data.data;
                    d.resolve(data.data);
                }, function errorCallback (error) {
                    d.reject(error);
                });
            }

            return d.promise;
        };
    })
}

