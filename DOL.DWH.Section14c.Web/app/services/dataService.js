module.exports = function ($http) {
    'use strict';

    function getNumbers() {
        var promise = $http.get('http://localhost:50014/api/example').then(function(response) {
            return response.data;
            //TODO add error handling and logging
        });
        return promise;
    }

    return {
        GetNumbers: getNumbers
    };
};