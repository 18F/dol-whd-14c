'use strict';

module.exports = function(ngModule) {
    require('./changePasswordForm')(ngModule);
    require('./mainHeaderControl')(ngModule);
    require('./mainNavigationControl')(ngModule);
    require('./userLoginForm')(ngModule);
    require('./userRegistrationForm')(ngModule);
};
