'use strict';

import * as _ from 'lodash';

module.exports = function(ngModule) {
  ngModule.service('submissionService', function() {
    'ngInject';
    'use strict';

    this.getSubmissionVM = function(ein, vm) {
      // replace "id" objects with a single ID property
      // deep clone so that we don't modify the passed object
      const submissionVm = _.cloneDeep(vm);
      forOwnDeep(submissionVm, function(value, key, object) {
        if (_.has(value, 'id')) {
          object[key + 'Id'] = value.id;
          delete object[key];
        }
        if(_.has(value, 'scaAttachmentsIds')) {
          object['scaAttachmentIds'].map(function(element){
            return element.attachmentId;
          })
        }
      });

      // add in EIN
      submissionVm.ein = ein;

      return submissionVm;
    };

    function forOwnDeep(obj, iteratee) {
      _.forOwn(obj, function(value, key) {
        iteratee(value, key, obj);
        if (_.isPlainObject(value)) {
          forOwnDeep(value, iteratee);
        }
        if (_.isArray(value)) {
          _.each(value, function(val) {
            forOwnDeep(val, iteratee);
          });
        }
      });
    }
  });
};
