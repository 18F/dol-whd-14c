'use strict';

module.exports = function(ngModule) {
    require('./changePasswordForm')(ngModule);
    require('./formFooterControls')(ngModule);
    require('./formSection')(ngModule);
    require('./mainHeaderControl')(ngModule);
    require('./mainNavigationControl')(ngModule);
    require('./sectionAppInfo')(ngModule);
    require('./sectionAssurances')(ngModule);
    require('./sectionEmployer')(ngModule);
    require('./sectionWorkSites')(ngModule);
    require('./sectionWageData')(ngModule);
    require('./sectionWioa')(ngModule);
    require('./userLoginForm')(ngModule);
    require('./userRegistrationForm')(ngModule);
};
