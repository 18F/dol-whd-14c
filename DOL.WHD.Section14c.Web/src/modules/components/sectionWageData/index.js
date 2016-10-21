'use strict';

module.exports = function(ngModule) {
    require('./wageDataPayTypeFormController')(ngModule);
    require('./wageDataPayTypeFormDirective')(ngModule);
    require('./sectionWageDataController')(ngModule);
    require('./sectionWageDataDirective')(ngModule);
};
