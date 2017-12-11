'use strict';

module.exports = function(ngModule) {
  ngModule.service('autoSaveService', function(
    $timeout,
    stateService,
    apiService
  ) {
    'ngInject';
    'use strict';

    let duration = 60 * 1000; // 60 seconds

    let start = function start() {
      nextTimer();
    };

    let save = function save(callback) {
      if (!stateService.access_token || !stateService.ein) {
        if (callback) callback();
        return undefined;
      }
      
      return apiService
        .saveApplication(
          stateService.access_token,
          stateService.ein,
          stateService.employerId,
          stateService.applicationId,
          stateService.formData
        )
        .then(
          function() {
            if (callback) callback();
          },
          function() {
            if (callback) callback();
            //Todo: Show Error
          }
        );
    };

    function nextTimer() {
      $timeout(
        function() {
          save(nextTimer);
        },
        duration,
        false
      );
    }

    return {
      start: start,
      save: save
    };
  });
};
