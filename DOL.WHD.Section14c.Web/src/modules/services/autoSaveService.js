'use strict';

module.exports = function(ngModule) {
    ngModule.service('autoSaveService', function($timeout, stateService, apiService) {
        'ngInject';
        'use strict';

        let duration =  60 * 1000; // 60 seconds
        let timer;

        let start = function start(){
            nextTimer();
        }

        let save = function save(callback){
            return apiService.saveApplication(stateService.access_token, stateService.ein, stateService.formData).then(function () {
                if(callback) callback();
                console.log("autosave");
            }, function() {
                if(callback) callback();
                //Todo: Show Error
                console.error("autosave failed");
            });
        }

        function nextTimer() {
            timer = $timeout(function () {
                console.log("timer save initiated");
                save(nextTimer)
            }, duration, false);
        }

        return {
            start: start,
            save: save
        }
    });
}
