'use strict';

module.exports = function(ngModule) {
  require('./adminReviewController')(ngModule);
  require('./adminReviewDirective')(ngModule);
};
