'use strict';

module.exports = function(ngModule) {
  require('./adminWageDataPayTypeDirective')(ngModule);
  require('./sectionAdminWageDataController')(ngModule);
  require('./sectionAdminWageDataDirective')(ngModule);
};
