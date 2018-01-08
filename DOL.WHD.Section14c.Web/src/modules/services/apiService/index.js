import passwordMethodsFactory from './password';
import applicationsMethodsFactory from './applications';
import accountsMethodsFactory from './accounts';

module.exports = function(ngModule) {
  ngModule.service('apiService', function(
    $http,
    $q,
    _env,
    moment,
    submissionService
  ) {
    'ngInject';

    const passwordMethods = passwordMethodsFactory(_env, $q, $http);
    this.changePassword = passwordMethods.changePassword;
    this.resetPassword = passwordMethods.resetPassword;
    this.verifyResetPassword = passwordMethods.verifyResetPassword;

    this.userRegister = function(
      ein,
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
          EIN: ein,
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
          //console.log(error);
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

    const applicationMethods = applicationsMethodsFactory(_env, $q, $http, moment, submissionService);
    this.saveApplication = applicationMethods.saveApplication;
    this.getApplication = applicationMethods.getApplication;
    this.getSubmittedApplication = applicationMethods.getSubmittedApplication;
    this.getSubmittedApplications = applicationMethods.getSubmittedApplications;
    this.submitApplication = applicationMethods.submitApplication;
    this.changeApplicationStatus = applicationMethods.changeApplicationStatus;

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
          //console.log(error);
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

    const accountsMethods = accountsMethodsFactory(_env, $q, $http);
    this.getAccounts = accountsMethods.getAccounts;
    this.getRoles = accountsMethods.getRoles;
    this.getAccount = accountsMethods.getAccount;
    this.modifyAccount = accountsMethods.modifyAccount;
    this.createAccount = accountsMethods.createAccount;

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
  });
};
