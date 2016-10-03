'use strict';

module.exports = function(ngModule) {
    ngModule.service('apiService', function($http, $q, _env) {
        'ngInject';
        'use strict';

        this.changePassword = function(email, oldPassword, newPassword, confirmPassword) {

            let url = _env.api_url + '/api/Account/ChangePassword';
            let d = $q.defer();

            $http({
                method: 'POST',
                url: url,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                data: $.param({"Email": email, "OldPassword": oldPassword, "NewPassword": newPassword, "ConfirmPassword": confirmPassword})
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };

        this.userLogin = function(email, password) {

            let url = _env.api_url + '/Token';
            let d = $q.defer();

            $http({
                method: 'POST',
                url: url,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                data: $.param({"grant_type": "password", "userName": email, "password": password})
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };

        this.userRegister = function(ein, email, password, confirmPassword) {

            let url = _env.api_url + '/api/Account/Register';
            let d = $q.defer();

            $http({
                method: 'POST',
                url: url,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                data: $.param({"EIN": ein, "Email": email, "Password": password, "ConfirmPassword": confirmPassword})
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };
    });
}
