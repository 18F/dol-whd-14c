'use strict';

module.exports = function(ngModule) {
    require('./accountManagementControls')(ngModule);
    require('./adminReview')(ngModule);
    require('./answerField')(ngModule);
    require('./attachmentField')(ngModule);
    require('./changePasswordForm')(ngModule);
    require('./dateField')(ngModule);
    require('./formFooterControls')(ngModule);
    require('./formSection')(ngModule);
    require('./mainHeaderControl')(ngModule);
    require('./mainNavigationControl')(ngModule);
    require('./resetPasswordForm')(ngModule);
    require('./sectionAdminAppInfo')(ngModule);
    require('./sectionAdminEmployer')(ngModule);
    require('./sectionAppInfo')(ngModule);
    require('./sectionAssurances')(ngModule);
    require('./sectionEmployer')(ngModule);
    require('./sectionReview')(ngModule);
    require('./sectionWageData')(ngModule);
    require('./sectionWioa')(ngModule);
    require('./sectionWorkSites')(ngModule);
    require('./stateField')(ngModule);
    require('./userLoginForm')(ngModule);
    require('./userRegistrationForm')(ngModule);
};
