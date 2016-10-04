'use strict';

module.exports = function(ngModule) {
    require('./changePasswordPageController')(ngModule);
    require('./landingPageController')(ngModule);
    require('./userLoginPageController')(ngModule);
    require('./userRegistrationPageController')(ngModule);
};
