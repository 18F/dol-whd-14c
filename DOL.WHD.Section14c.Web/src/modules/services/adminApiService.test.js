/* eslint-disable max-statements */
describe('adminApiService', function() {
  var api, $httpBackend, env;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($injector, _$httpBackend_, adminApiService, moment, submissionService, _env) {
      api = adminApiService;
      $httpBackend = _$httpBackend_;
      env = _env;
    })
  );


  //resetPassword
  it('resetPassword error should reject deferred', function() {
    var isResolved;
    var result;
    api.resetPassword().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AccountAdmin/ResetPassword')
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
      .expectPOST(env.api_url + '/api/Account/AccountAdmin/ResetPassword')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  it('resendConfirmationEmail error should reject deferred', function() {
    var isResolved;
    var result;
    var userId = '1234';
    api.resendConfirmationEmail(undefined, userId).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AccountAdmin/ResendConfirmationEmail?userId=' + userId)
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('resendConfirmationEmail success should resolve deferred', function() {
    var isResolved;
    var result;
    var userId = '1234';
    api.resendConfirmationEmail(undefined, userId).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AccountAdmin/ResendConfirmationEmail?userId=' + userId)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  it('resendCode error should reject deferred', function() {
    var isResolved;
    var result;
    var userId = '1234';
    api.resendCode(undefined, userId).then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AccountAdmin/ResendCode?userId=' + userId)
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('resendCode success should resolve deferred', function() {
    var isResolved;
    var result;
    var userId = '1234'
    api.resendCode(undefined, userId).then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AccountAdmin/ResendCode?userId=' + userId)
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });
});
