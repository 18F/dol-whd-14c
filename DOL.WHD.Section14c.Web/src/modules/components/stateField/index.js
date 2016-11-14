'use strict';

module.exports = function(ngModule) {
    require('./stateFieldController')(ngModule);
    require('./stateFieldDirective')(ngModule);
};