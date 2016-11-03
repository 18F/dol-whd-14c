describe('apiService', function() {

    beforeEach(module('14c'));

    var api;

    beforeEach(inject(function($injector) {
        responses = $injector.get('apiService');
    }));

    it('should call the changePassword method', function() {
        api.changePassword(email, oldPassword, newPassword, confirmPassword);
        expect(api.changePassword).toEqual('value');
    });

    it('should call the userLogin method', function() {
        api.userLogin(current);
        expect(api.userLogin).toEqual('value');
    });

    it('should call the userRegister method', function() {
        api.userRegister(current);
        expect(api.userRegister).toEqual('value');
    });

    it('should call the userInfo method', function() {
        api.userInfo(current);
        expect(api.userInfo).toEqual('value');
    });

        it('should call the saveApplication method', function() {
        api.saveApplication(current);
        expect(api.saveApplication).toEqual('value');
    });

        it('should call the getApplication method', function() {
        api.getApplication(current);
        expect(api.getApplication).toEqual('value');
    });

        it('should call the uploadAttachment method', function() {
        api.uploadAttachment(current);
        expect(api.uploadAttachment).toEqual('value');
    });

        it('should call the deleteAttachment method', function() {
        api.deleteAttachment(current);
        expect(api.deleteAttachment).toEqual('value');
    });

});