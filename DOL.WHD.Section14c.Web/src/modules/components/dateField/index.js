'use strict';

module.exports = function(ngModule) {
    require('./dateFieldController')(ngModule);
    require('./dateFieldDirective')(ngModule);
};