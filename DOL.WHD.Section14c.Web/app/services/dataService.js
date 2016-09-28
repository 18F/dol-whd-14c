module.exports = function ($http, apiUrl) {
    'use strict';

    function getNumbers() {
        var promise = $http.get(apiUrl + '/api/example').then(function(response) {
            return response.data;
            //TODO add error handling and logging
        });
        return promise;
    }

    return {
        GetNumbers: getNumbers
    };
};