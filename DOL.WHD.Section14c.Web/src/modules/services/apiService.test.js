describe('apiService', function() {

    beforeEach(module('14c'));

    var api;
    var id;
    var file;
    var ein;
    var userId;
    var email;
    var password;
    var oldPassword;
    var newPassword;
    var confirmPassword;
    var access_token;
    var $http;
    var passwordResetUrl;
    var current;
    var code;
    var reCaptchaResponse;
    var emailVerificationUrl;
    var applicationData = { saved : 'place' };
    var account = { email : 'jonhson', roles : 'admin', userId : 'jmmcnj'};
    var response = { modelstate : { error : 'something went wrong'}};
    beforeEach(inject(function($injector, _$http_) {
        api = $injector.get('apiService');
        $http = _$http_;
        spyOn($http, 'post');
    }));

    it('should call the changePassword method', function() {
        
        api.changePassword(access_token, email, oldPassword, newPassword, confirmPassword);
        //expect(api.changePassword).toEqual('value');
    });

    it('should call the resetPassword method', function() {
        
        api.resetPassword(email, passwordResetUrl);
        //expect(api.resetPasword).toEqual('value');
    });

    it('should call the verifyResetPassword method', function() {

        api.verifyResetPassword(userId, newPassword, confirmPassword, code);
        //expect(api.verifyResetPasword).toEqual('value');
    });

    it('should call the userLogin method', function() {

        api.userLogin(email, password);
        //expect(api.userLogin).toEqual('value');
    });

    it('should call the userRegister method', function() {

        api.userRegister(ein, email, password, confirmPassword, reCaptchaResponse, emailVerificationUrl);
        //expect(api.userRegister).toEqual('value');
    });

    it('should call the emailVerification method', function() {

        api.emailVerification(userId, code, reCaptchaResponse);
        //expect(api.emailVerification).toEqual('value');
    });

    it('should call the userInfo method', function() {
        
        api.userInfo(access_token);
        //expect(api.userInfo).toEqual('value');
    });

    it('should call the saveApplication method', function() {
        
        api.saveApplication(access_token, ein, applicationData);
        //expect(api.saveApplication).toEqual('value');
    });

    it('should call the getApplication method', function() {
        
        api.getApplication(access_token, ein, applicationData) ;
        //expect(api.getApplication).toEqual('value');
    });

    it('should call the uploadAttachment method', function() {
        
        api.uploadAttachment(access_token, ein, file);
        //expect(api.uploadAttachment).toEqual('value');
    });

    it('should call the deleteAttachment method', function() {
        
        api.deleteAttachment(access_token, ein, id);
        //expect(api.deleteAttachment).toEqual('value');
    });

    it('should call the getAccounts method', function() {
        
        api.getAccounts(access_token);
        //expect(api.getAccounts).toEqual('value');
    });

    it('should call the getRoles method', function() {
        
        api.getRoles(access_token);
        //expect(api.getRoles).toEqual('value');
    });

    it('should call the getAccount method', function() {
        
        api.getAccount(access_token);
        //expect(api.getAccount).toEqual('value');
    });

    it('should call the modifyAccount method', function() {
        
        api.modifyAccount(access_token, account);
        //expect(api.modifyAccount).toEqual('value');
    });

    it('should call the createAccount method', function() {
        
        api.createAccount(access_token, account);
        //expect(api.createAccount).toEqual('value');
    });

    it('should call the parseErrors method', function() {
        
        api.parseErrors(response);
        //expect(api.parseErrors).toEqual('value');
    });
});