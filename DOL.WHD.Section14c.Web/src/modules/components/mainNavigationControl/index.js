'use strict';

module.exports = function(ngModule) {
    require('./mainNavigationControlController')(ngModule);
    require('./mainNavigationControlDirective')(ngModule);
};
