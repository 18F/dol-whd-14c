'use strict';

module.exports = function(ngModule) {
    require('./userLoginController')(ngModule);
    require('./userLoginDirective')(ngModule);
};
