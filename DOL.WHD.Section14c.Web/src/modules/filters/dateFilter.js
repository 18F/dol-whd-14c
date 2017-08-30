'use strict';

module.exports = function(ngModule) {
  ngModule.filter('dateFilter', function(moment) {
    return function(input) {
      // parse to moment object
      let outVal = 'INVALID DATE';
      const dateValMoment = moment(input, moment.ISO_8601, true);
      if (dateValMoment.isValid()) {
        // parse out values
        const month = dateValMoment.month() + 1; // month is zero-based
        const day = dateValMoment.date();
        const year = dateValMoment.year();
        outVal = `${month}/${day}/${year}`;
      }

      return outVal;
    };
  });
};
