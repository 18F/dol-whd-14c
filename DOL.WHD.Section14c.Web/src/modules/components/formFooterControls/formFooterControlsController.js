'use strict';

module.exports = function(ngModule) {
    ngModule.controller('formFooterControlsController', function($scope, $location, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;

        this.doSave = function() {
            apiService.saveApplication(stateService.formData);
        }

        this.onNextClick = function() {
            this.doSave();

            //TODO: navigate to next section
        }

        this.onBackClick = function() {
            this.doSave();

            //TODO: navigate to prev section
        }

        this.onSaveClick = function() {
            this.doSave();

            //TODO: exit application form
        }
    });
}
