'use strict';

module.exports = function(ngModule) {
  ngModule.service('apiService', function(
    $http,
    $q,
    _env,
    moment,
    submissionService
  ) {
    'ngInject';
    'use strict';

    this.attachmentApiURL = _env.api_url + '/api/attachment/';

    this.changePassword = function(
      access_token,
      email,
      oldPassword,
      newPassword,
      confirmPassword
    ) {
      let url = _env.api_url + '/api/Account/ChangePassword';
      let d = $q.defer();
      let headerVal;

      if (access_token !== undefined) {
        headerVal = {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        };
      } else {
        headerVal = { 'Content-Type': 'application/x-www-form-urlencoded' };
      }

      $http({
        method: 'POST',
        url: url,
        headers: headerVal,
        data: $.param({
          Email: email,
          OldPassword: oldPassword,
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

    this.resetPassword = function(email, passwordResetUrl) {
      let url = _env.api_url + '/api/Account/ResetPassword';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        data: $.param({ Email: email, PasswordResetUrl: passwordResetUrl })
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

    this.verifyResetPassword = function(
      userId,
      newPassword,
      confirmPassword,
      code
    ) {
      let url = _env.api_url + '/api/Account/VerifyResetPassword';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        data: $.param({
          NewPassword: newPassword,
          ConfirmPassword: confirmPassword,
          UserId: userId,
          Nounce: code
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

    this.userRegister = function(
      firstName,
      lastName,
      email,
      password,
      confirmPassword,
      emailVerificationUrl
    ) {
      let url = _env.api_url + '/api/Account/Register';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        data: $.param({
          FirstName: firstName,
          LastName: lastName,
          Email: email,
          Password: password,
          ConfirmPassword: confirmPassword,
          EmailVerificationUrl: emailVerificationUrl
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

    this.emailVerification = function(userId, code) {
      let url = _env.api_url + '/api/Account/VerifyEmail';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        data: $.param({
          UserId: userId,
          Nounce: code
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

    this.checkPasswordComplexity = function(value) {
      let url = _env.api_url + '/api/Account/PasswordComplexityCheck';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        data: value
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

    this.userInfo = function(access_token) {
      let url = _env.api_url + '/api/Account/UserInfo';
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token
        }
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

    this.saveApplication = function(access_token, ein, applicationData) {
      let url = _env.api_url + '/api/save/' + ein;
      let d = $q.defer();

      applicationData.saved = moment.utc();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: applicationData
      }).then(
        function successCallback(data) {
          applicationData.lastSaved = moment.utc();
          d.resolve(data);
        },
        function errorCallback(error) {
          applicationData.lastSaved = 0;
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.getApplication = function(access_token, ein) {
      let url = _env.api_url + '/api/save/' + ein;
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token
        }
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

    this.getSubmittedApplication = function(access_token, appid) {
      let url = _env.api_url + '/api/application?id=' + appid;
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token
        }
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

    this.getSubmittedApplications = function(access_token) {
      let url = _env.api_url + '/api/application/summary';
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        }
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

    this.uploadAttachment = function(access_token, ein, file) {
      let url = _env.api_url + '/api/attachment/' + ein;
      let d = $q.defer();

      let fd = new FormData();
      fd.append('file', file);

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': undefined
        },
        data: fd
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

    this.deleteAttachment = function(access_token, ein, id) {
      let url = _env.api_url + '/api/attachment/' + ein + '/' + id;
      let d = $q.defer();

      $http({
        method: 'DELETE',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token
        }
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

    this.getAccounts = function(access_token) {
      let url = _env.api_url + '/api/account';
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        }
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

    this.getRoles = function(access_token) {
      let url = _env.api_url + '/api/account/roles';
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        }
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

    this.getAccount = function(access_token, userId) {
      let url = _env.api_url + '/api/account/' + userId;
      let d = $q.defer();

      $http({
        method: 'GET',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        }
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

    this.modifyAccount = function(access_token, account) {
      let url = _env.api_url + '/api/account/' + account.userId;
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: $.param({ Email: account.email, Roles: account.roles })
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

    this.createAccount = function(access_token, account) {
      let url = _env.api_url + '/api/account';
      let d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token,
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: $.param({ Email: account.email, Roles: account.roles })
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

    this.parseErrors = function(response) {
      var errors = [];
      if (response.modelState !== undefined) {
        for (var key in response.modelState) {
          if (response.modelState[key] !== undefined) {
            for (var i = 0; i < response.modelState[key].length; i++) {
              errors.push(response.modelState[key][i]);
            }
          }
        }
      }
      return errors;
    };

    this.submitApplication = function(access_token, ein, vm) {
      const url = _env.api_url + '/api/application/submit';
      const d = $q.defer();
      const submissionVm = submissionService.getSubmissionVM(ein, vm);

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token
        },
        data: submissionVm
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

    this.changeApplicationStatus = function(access_token, appId, newStatusId) {
      const url = `${_env.api_url}/api/application/status?id=${appId}&statusId=${newStatusId}`;
      const d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: {
          Authorization: 'bearer ' + access_token
        }
      }).then(
        function successCallback() {
          d.resolve();
        },
        function errorCallback(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };
  });
};
