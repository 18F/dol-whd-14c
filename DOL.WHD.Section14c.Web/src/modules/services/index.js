'use strict';

module.exports = function(ngModule) {
  require('./authService')(ngModule);
  require('./apiService')(ngModule);
  require('./assetService')(ngModule);
  require('./responsesService')(ngModule);
  require('./stateService')(ngModule);
  require('./navService')(ngModule);
  require('./autoSaveService')(ngModule);
  require('./validationService')(ngModule);
  require('./submissionService')(ngModule);
  require('./statusesService')(ngModule);
  require('./errorLogService')(ngModule);
};
