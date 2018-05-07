'use strict';

module.exports = function(ngModule) {
  require('./auditAccountsController')(ngModule);
  require('./auditAccountsDirective')(ngModule);
};
