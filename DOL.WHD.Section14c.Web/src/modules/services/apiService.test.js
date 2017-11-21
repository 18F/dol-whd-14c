/* eslint-disable max-statements */
describe('apiService', function() {
  beforeEach(module('14c'));

  var api, $httpBackend, env;
  var userId;
  var access_token;

  beforeEach(
    inject(function($injector, _$httpBackend_, apiService, _env) {
      api = apiService;
      $httpBackend = _$httpBackend_;
      env = _env;
    })
  );

  //changePassword
  it('changePassword error should reject deferred', function() {
    var isResolved;
    var result;
    var access_token = 'token';
    api.changePassword(access_token).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/ChangePassword')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('changePassword success should resolve deferred', function() {
    var isResolved;
    var result;
    api.changePassword().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/ChangePassword')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //resetPassword
  it('resetPassword error should reject deferred', function() {
    var isResolved;
    var result;
    api.resetPassword().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/ResetPassword')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('resetPassword success should resolve deferred', function() {
    var isResolved;
    var result;
    api.resetPassword().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/ResetPassword')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //verifyResetPassword
  it('verifyResetPassword  error should reject deferred', function() {
    var isResolved;
    var result;
    api.verifyResetPassword().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/VerifyResetPassword')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('verifyResetPassword  success should resolve deferred', function() {
    var isResolved;
    var result;
    api.verifyResetPassword().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/VerifyResetPassword')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //userRegister
  it('userLogin error should reject deferred', function() {
    var isResolved;
    var result;
    api.userRegister().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/Register')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('userLogin success should resolve deferred', function() {
    var isResolved;
    var result;
    api.userRegister().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/Register')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //emailVerification
  it('emailVerification error should reject deferred', function() {
    var isResolved;
    var result;
    api.emailVerification().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/VerifyEmail')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('emailVerification success should resolve deferred', function() {
    var isResolved;
    var result;
    api.emailVerification().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/VerifyEmail')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //userInfo
  it('userInfo error should reject deferred', function() {
    var isResolved;
    var result;
    api.userInfo().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('userInfo success should resolve deferred', function() {
    var isResolved;
    var result;
    api.userInfo().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/Account/UserInfo')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //saveApplication
  it('saveApplication error should reject deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    var applicationData = {};
    api
      .saveApplication(access_token, ein, applicationData)
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend
      .expectPOST(env.api_url + '/api/save/' + ein)
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
    expect(applicationData.lastSaved).toEqual(0);
  });

  it('saveApplication success should resolve deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    var applicationData = {};
    api
      .saveApplication(access_token, ein, applicationData)
      .then(function(data) {
        result = data.data;
        isResolved = true;
      });

    $httpBackend
      .expectPOST(env.api_url + '/api/save/' + ein)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
    expect(applicationData.lastSaved).not.toEqual(0);
  });

  //getApplication
  it('getApplication error should reject deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    api.getApplication(access_token, ein).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/save/' + ein)
      .respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('getApplication success should resolve deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    api.getApplication(access_token, ein).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/save/' + ein)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //uploadAttachment
  it('uploadAttachment error should reject deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    api.uploadAttachment(access_token, ein).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/attachment/' + ein)
      .respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('uploadAttachment success should resolve deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    api.uploadAttachment(access_token, ein).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/attachment/' + ein)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //deleteAttachment
  it('deleteAttachment error should reject deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    var id = '1';
    api
      .deleteAttachment(access_token, ein, id)
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend
      .expectDELETE(env.api_url + '/api/attachment/' + ein + '/' + id)
      .respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('deleteAttachment success should resolve deferred', function() {
    var isResolved;
    var result;
    var ein = '12-1234567';
    var id = '1';
    api.deleteAttachment(access_token, ein, id).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectDELETE(env.api_url + '/api/attachment/' + ein + '/' + id)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //getAccounts
  it('getAccounts error should reject deferred', function() {
    var isResolved;
    var result;
    api.getAccounts().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend.expectGET(env.api_url + '/api/account').respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('getAccounts success should resolve deferred', function() {
    var isResolved;
    var result;
    api.getAccounts().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend.expectGET(env.api_url + '/api/account').respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //getRoles
  it('getRoles error should reject deferred', function() {
    var isResolved;
    var result;
    api.getRoles().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/account/roles')
      .respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('getRoles success should resolve deferred', function() {
    var isResolved;
    var result;
    api.getRoles().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/account/roles')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //getAccounts
  it('getAccounts error should reject deferred', function() {
    var isResolved;
    var result;
    var userId = '1';
    api.getAccount(access_token, userId).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/account/' + userId)
      .respond(404, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('getAccounts success should resolve deferred', function() {
    var isResolved;
    var result;
    api.getAccount(access_token, userId).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/account/' + userId)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //modifyAccount
  it('modifyAccount error should reject deferred', function() {
    var isResolved;
    var result;
    var account = { userId: '1' };
    api.modifyAccount(access_token, account).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/account/' + account.userId)
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('modifyAccount success should resolve deferred', function() {
    var isResolved;
    var result;
    var account = { userId: '1' };
    api.modifyAccount(access_token, account).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/account/' + account.userId)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //createAccount
  it('modifyAccount error should reject deferred', function() {
    var isResolved;
    var result;
    var account = { userId: '1' };
    api.createAccount(access_token, account).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend.expectPOST(env.api_url + '/api/account').respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('createAccount success should resolve deferred', function() {
    var isResolved;
    var result;
    var account = { userId: '1' };
    api.createAccount(access_token, account).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend.expectPOST(env.api_url + '/api/account').respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  it('should call the parseErrors method', function() {
    var errors = api.parseErrors({ modelState: { error: ['message'] } });
    expect(errors.length).toEqual(1);
    expect(errors[0]).toEqual('message');
  });

  it('should call the parseErrors method', function() {
    var errors = api.parseErrors({ modelState: {} });
    expect(errors.length).toEqual(0);
  });

  //submitApplication
  it('submitApplication error should reject deferred', function() {
    var isResolved;
    var result;
    var ein = '30-1234567';
    api
      .submitApplication(access_token, ein, {})
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend
      .expectPOST(env.api_url + '/api/application/submit')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('submitApplication success should resolve deferred', function() {
    var isResolved;
    var result;
    var ein = '30-1234567';
    api.submitApplication(access_token, ein, {}).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/application/submit')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  //getSubmittedApplication
  it('getSubmittedApplication error should reject deferred', function() {
    var isResolved;
    var result;
    var appid = '12345';
    api
      .getSubmittedApplication(access_token, appid, {})
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend
      .expectGET(env.api_url + '/api/application?id=' + appid)
      .respond(404, 'not found');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('not found');
  });

  it('getSubmittedApplication success should resolve deferred', function() {
    var isResolved;
    var result;
    var appid = '12345';
    var ein = '30-1234567';
    api.getSubmittedApplication(access_token, appid, {}).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/application?id=' + appid)
      .respond(200, { ein: ein });
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result.ein).toEqual(ein);
  });

  //getSubmittedApplications
  it('getSubmittedApplications error should reject deferred', function() {
    var isResolved;
    var result;
    api
      .getSubmittedApplications(access_token, {})
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend
      .expectGET(env.api_url + '/api/application/summary')
      .respond(404, 'not found');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('not found');
  });

  it('getSubmittedApplications success should resolve deferred', function() {
    var isResolved;
    var result;
    var ein = '30-1234567';
    api.getSubmittedApplications(access_token, {}).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectGET(env.api_url + '/api/application/summary')
      .respond(200, [{ ein: ein }]);
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result[0].ein).toEqual(ein);
  });

  //changeApplicationStatus
  it('changeApplicationStatus error should reject deferred', function() {
    var isResolved;
    var result;
    var appid = '12345';
    var newStatusId = '5';
    api
      .changeApplicationStatus(access_token, appid, newStatusId)
      .then(undefined, function(error) {
        result = error.data;
        isResolved = false;
      });

    $httpBackend
      .expectPOST(
        env.api_url +
          '/api/application/status?id=' +
          appid +
          '&statusId=' +
          newStatusId
      )
      .respond(400, 'error');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('error');
  });

  it('changeApplicationStatus success should resolve deferred', function() {
    var isResolved;
    var appid = '12345';
    var newStatusId = '5';
    api
      .changeApplicationStatus(access_token, appid, newStatusId)
      .then(function() {
        isResolved = true;
      });

    $httpBackend
      .expectPOST(
        env.api_url +
          '/api/application/status?id=' +
          appid +
          '&statusId=' +
          newStatusId
      )
      .respond(200);
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
  });
});
