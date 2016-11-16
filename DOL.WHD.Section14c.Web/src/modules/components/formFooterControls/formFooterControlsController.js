'use strict';

module.exports = function(ngModule) {
    ngModule.controller('formFooterControlsController', function($scope, $location, $route, navService, autoSaveService, validationService) {
        'ngInject';
        'use strict';

        $scope.navService = navService;

        var vm = this;
        vm.hasNext = navService.hasNext();
        vm.hasBack = navService.hasBack();

        $scope.$watch('navService.nextLabel', function(value) {
            vm.setNextLabel(value ? value : undefined);
        });

        $scope.$watch('navService.backLabel', function(value) {
            vm.setBackLabel(value ? value : undefined);
        });

        $scope.$watch('navService.hasNext()', function(value) {
            vm.hasNext = value;
        });

        $scope.$watch('navService.hasBack()', function(value) {
            vm.hasBack = value;
        });

        this.setNextLabel = function(label) {
            vm.nextLabel = label ? label : navService.getNextSection() === "review" ? "Review & Submit Application" : "Next";
        }

        this.setBackLabel = function(label) {
            vm.backLabel = label ? label : "Back";
        }

        this.doSave = function() {
            autoSaveService.save();
        }

        this.onNextClick = function() {
            this.doSave();

            if (this.hasNext) {
                navService.goNext();
            }
        }

        this.onBackClick = function() {
            this.doSave();

            if (this.hasBack) {
                navService.goBack();
            }
        }

        this.onSaveClick = function() {
            this.doSave();

            //TODO: exit application form
        }
    });
}
