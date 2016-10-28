'use strict';

module.exports = function(ngModule) {
  require('./apiService')(ngModule);
  require('./responsesService')(ngModule);
  require('./stateService')(ngModule);
  require('./navService')(ngModule);
  require('./autoSaveService')(ngModule);
  require('./validationService')(ngModule);
};
