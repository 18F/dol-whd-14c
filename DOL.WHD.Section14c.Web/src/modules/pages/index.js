'use strict';

module.exports = function(ngModule) {
    require('./appMainPageController')(ngModule);
    require('./appAboutPageController')(ngModule);
};
