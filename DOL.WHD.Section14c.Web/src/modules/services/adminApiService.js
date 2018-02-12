'use strict';

module.exports = function(ngModule) {
  ngModule.service('adminApiService', function(
    $http,
    $q,
    _env
  ) {
    'ngInject';
    'use strict';

    this.resendConfirmationEmail = function(access_token, userId) {
      let url = _env.api_url + '/api/Account/AccountAdmin/ResendConfirmationEmail';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: $.param({ userId: userId})
      }).then(
        function successCallback(data) {
          d.resolve(data);
        },
        function errorCallback(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.resetPassword = function(access_token, email, newPassword, confirmPassword) {
      let url = _env.api_url + '/api/Account/AccountAdmin/ResetPassword';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: $.param({
          Email: email,
          NewPassword: newPassword,
          ConfirmPassword: confirmPassword
        })
      }).then(
        function successCallback(data) {
          d.resolve(data);
        },
        function errorCallback(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.resendCode = function(access_token, userId) {
      let url = _env.api_url + '/api/Account/AccountAdmin/ResendCode';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: $.param({ userId: userId})
      }).then(
        function successCallback(data) {
          d.resolve(data);
        },
        function errorCallback(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };
  });
};
