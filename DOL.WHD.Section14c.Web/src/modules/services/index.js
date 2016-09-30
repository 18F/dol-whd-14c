'use strict';

module.exports = function(ngModule) {
  require('./apiService')(ngModule);
  require('./stateService')(ngModule);
};
