'use strict';

module.exports = function(ngModule) {
    ngModule.controller('formFooterControlsController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;

        this.doSave = function() {
            console.log(stateService.formData);
        }

        this.onNextClick = function() {
            this.doSave();
        }

        this.onBackClick = function() {
            this.doSave();
        }
    });
}
