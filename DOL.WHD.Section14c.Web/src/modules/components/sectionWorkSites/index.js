'use strict';

module.exports = function(ngModule) {
  require('./sectionWorkSitesController')(ngModule);
  require('./sectionWorkSitesDirective')(ngModule);
  require('./employeeWageDataTableDirective')(ngModule);  
};
