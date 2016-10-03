'use strict';

module.exports = function(ngModule) {
    require('./changePasswordForm')(ngModule);
    require('./userLogin')(ngModule);
    require('./userRegistrationForm')(ngModule);
};
