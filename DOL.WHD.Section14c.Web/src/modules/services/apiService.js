'use strict';

module.exports = function(ngModule) {
    ngModule.service('apiService', function($http, $q) {
        'ngInject';
        'use strict';

        this.userLogin = function(username, password) {

            let url = 'http://dol-whd-section14c-api-dev.azurewebsites.net/Token';
            let d = $q.defer();

            $http({
                method: 'POST',
                url: url,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                data: $.param({"grant_type": "password", "username": username, "password": password})
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                console.log(error);
                d.reject(error);
            });

            return d.promise;
        };
    });
}
