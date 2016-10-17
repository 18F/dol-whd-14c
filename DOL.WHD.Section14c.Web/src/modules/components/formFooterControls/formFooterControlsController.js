'use strict';

module.exports = function(ngModule) {
    ngModule.controller('formFooterControlsController', function($scope, $location, $route, stateService, apiService) {
        'ngInject';
        'use strict';

        var vm = this;
        vm.next = stateService.getNextSection($route.current.params.section_id);
        vm.previous = stateService.getPreviousSection($route.current.params.section_id);

        this.doSave = function() {
            apiService.saveApplication(stateService.access_token, stateService.ein, stateService.formData);
        }

        this.onNextClick = function() {
            this.doSave();

            if (this.next) {
                $location.path("/section/" + this.next);
            }
            else {
                //TODO: do submit
            }
        }

        this.onBackClick = function() {
            this.doSave();

            if (this.previous) {
                $location.path("/section/" + this.previous);
            }
        }

        this.onSaveClick = function() {
            this.doSave();

            //TODO: exit application form
        }
    });
}
