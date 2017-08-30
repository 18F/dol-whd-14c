'use strict';

module.exports = function(ngModule) {
  require('./accountGridController')(ngModule);
  require('./accountGridDirective')(ngModule);

  require('./accountFormController')(ngModule);
  require('./accountFormDirective')(ngModule);

  require('./accountCreateButtonController')(ngModule);
  require('./accountCreateButtonDirective')(ngModule);
};
