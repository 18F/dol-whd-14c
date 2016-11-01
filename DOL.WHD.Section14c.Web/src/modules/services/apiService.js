'use strict';

var moment = require('moment');

module.exports = function(ngModule) {
    ngModule.service('apiService', function($http, $q, _env) {
        'ngInject';
        'use strict';

        this.attachmentApiURL = _env.api_url + "/api/attachment/";

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

        this.userRegister = function(ein, email, password, confirmPassword, reCaptchaResponse, emailVerificationUrl) {

            let url = _env.api_url + '/api/Account/Register';
            let d = $q.defer();

            $http({
                method: 'POST',
                url: url,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                data: $.param({"EIN": ein, "Email": email, "Password": password, "ConfirmPassword": confirmPassword, "ReCaptchaResponse": reCaptchaResponse, "EmailVerificationUrl": emailVerificationUrl })
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };

        this.emailVerification = function(userId, code, reCaptchaResponse) {

            let url = _env.api_url + '/api/Account/VerifyEmail';
            let d = $q.defer();

            $http({
                method: 'POST',
                url: url,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                data: $.param({"UserId": userId, "Nounce": code, "ReCaptchaResponse": reCaptchaResponse })
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };


       this.userInfo = function(access_token) {

            let url = _env.api_url + '/api/Account/UserInfo';
            let d = $q.defer();

            $http({
                method: 'GET',
                url: url,
                headers: {
                    'Authorization': 'bearer ' + access_token
                }
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };

        this.saveApplication = function(access_token, ein, applicationData) {
            let url = _env.api_url + '/api/save/' + ein;
            let d = $q.defer();
            
            applicationData.saved = moment.utc();

            $http({
                method: 'POST',
                url: url,
                headers: {
                    'Authorization': 'bearer ' + access_token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: applicationData
            }).then(function successCallback (data) {
                applicationData.lastSaved = moment.utc();
                d.resolve(data);
            }, function errorCallback (error) {
                applicationData.lastSaved = 0;
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        }

        this.getApplication = function(access_token, ein, applicationData) {
            let url = _env.api_url + '/api/save/' + ein;
            let d = $q.defer();

            $http({
                method: 'GET',
                url: url,
                headers: {
                    'Authorization': 'bearer ' + access_token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: applicationData
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        }

        this.uploadAttachment = function(access_token, ein, file) {

            let url = _env.api_url + '/api/attachment/' + ein;
            let d = $q.defer();

            let fd = new FormData();
            fd.append('file', file);

            $http({
                method: 'POST',
                url: url,
                headers: {
                    'Authorization': 'bearer ' + access_token,
                    'Content-Type': undefined
                },
                data: fd
            }).then(function successCallback (data) {
                d.resolve(data);
            }, function errorCallback (error) {
                //console.log(error);
                d.reject(error);
            });

            return d.promise;
        };

       this.deleteAttachment = function(access_token, ein, id) {

            let url = _env.api_url + '/api/attachment/' + ein + '/' + id;
            let d = $q.defer();

            $http({
                method: 'DELETE',
                url: url,
                headers: {
                    'Authorization': 'bearer ' + access_token
                }
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
