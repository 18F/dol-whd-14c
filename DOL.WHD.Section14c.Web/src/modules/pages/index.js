'use strict';

module.exports = function(ngModule) {
    require('./accountPageController')(ngModule);
    require('./changePasswordPageController')(ngModule);
    require('./landingPageController')(ngModule);
    require('./userLoginPageController')(ngModule);
    require('./userRegistrationPageController')(ngModule);
    require('./forgotPasswordPageController')(ngModule);
};
