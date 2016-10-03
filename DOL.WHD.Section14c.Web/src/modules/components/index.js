'use strict';

module.exports = function(ngModule) {
    require('./changePasswordForm')(ngModule);
    require('./userLoginForm')(ngModule);
    require('./userRegistrationForm')(ngModule);
};
