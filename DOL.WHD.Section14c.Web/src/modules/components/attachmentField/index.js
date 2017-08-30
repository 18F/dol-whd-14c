'use strict';

module.exports = function(ngModule) {
  require('./attachmentFieldController')(ngModule);
  require('./attachmentFieldDirective')(ngModule);
};
