'use strict';

module.exports = function(ngModule) {
    require('./changePasswordForm')(ngModule);
    require('./formFooterControls')(ngModule);
    require('./formSection')(ngModule);
    require('./mainHeaderControl')(ngModule);
    require('./mainNavigationControl')(ngModule);
    require('./sectionWageData')(ngModule);
    require('./userLoginForm')(ngModule);
    require('./userRegistrationForm')(ngModule);
};
