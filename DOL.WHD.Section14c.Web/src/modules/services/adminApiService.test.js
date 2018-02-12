/* eslint-disable max-statements */
describe('adminApiService', function() {
  var api, $httpBackend, env;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($injector, _$httpBackend_, adminApiService, moment, submissionService, _env) {
      api = apiService;
      submissionService = submissionService;
      moment = moment;
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
      .expectPOST(env.api_url + '/api/Account/AdminAccount/ResetPassword')
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
      .expectPOST(env.api_url + '/api/Account/AdminAccount/ResetPassword')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  it('resendConfirmationEmail error should reject deferred', function() {
    var isResolved;
    var result;
    api.resendConfirmationEmail().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AdminAccount/ResendConfirmationEmail')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('resendConfirmationEmail success should resolve deferred', function() {
    var isResolved;
    var result;
    api.resetPassword().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AdminAccount/ResendConfirmationEmail')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });

  it('resetPassword error should reject deferred', function() {
    var isResolved;
    var result;
    api.resendCode().then(undefined, function(error) {
      result = error.data;
      isResolved = false;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AdminAccount/ResendCode')
      .respond(400, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(false);
    expect(result).toEqual('value');
  });

  it('resendCode success should resolve deferred', function() {
    var isResolved;
    var result;
    api.resendCode().then(function(data) {
      result = data.data;
      isResolved = true;
    });

    $httpBackend
      .expectPOST(env.api_url + '/api/Account/AdminAccount/ResendCode')
      .respond(200, 'value');
    $httpBackend.flush();
    expect(isResolved).toEqual(true);
    expect(result).toEqual('value');
  });


});
