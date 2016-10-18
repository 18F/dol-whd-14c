'use strict';

module.exports = function(ngModule) {
  require('./apiService')(ngModule);
  require('./responsesService')(ngModule);
  require('./stateService')(ngModule);
};
