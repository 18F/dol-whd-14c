'use strict';

import isEmpty from 'lodash/isEmpty';
import merge from 'lodash/merge';
import find from 'lodash/find';

module.exports = function(ngModule) {
  ngModule.controller('sectionWorkSitesController', function(
    $scope,
    $location,
    $document,
    navService,
    responsesService,
    stateService,
    validationService,
    _constants
  ) {
    'ngInject';
    'use strict';

    $scope.formData = stateService.formData;
    $scope.validate = validationService.getValidationErrors;
    $scope.showAllHelp = false;

    if (!$scope.formData.workSites) {
      $scope.formData.workSites = [];
    }

    let query = $location.search();


    var vm = this;
    vm.activeTab = query.t ? query.t : 1;
    vm.activeWorksite = {};
    vm.activeWorksiteIndex = -1;
    vm.activeWorker = {};
    vm.activeWorkerIndex = -1;
    vm.exampleView = "1";
    vm.saveEmployee = {
      status: false,
      name: ''
    };
    vm.columns = [
      {
          "className": '',
          "orderable": false,
          "data":null,
          "defaultContent": ""
      },
      { title: 'Name', model: 'name' },
      { title: 'Type of work performed', model: 'workType'  },
      { title: 'Primary disability', model: 'primaryDisabilityId'  },
      { title: 'How many jobs did this worker perform at this work site?', model: 'numJobs'  },
      { title: 'Average # of hours worked per week on all jobs at this work site', model: 'avgWeeklyHours'  },
      { title: 'Average earnings per hour for all jobs at this work site', model: 'avgHourlyEarnings'  },
      { title: 'Prevailing wage rate for job described above', model: 'prevailingWage'  },
      { title: 'Productivity measure/rating for job described above', model: 'hasProductivityMeasure'  },
      { title: 'Commensurate wage rate/average earnings per hour for job described above', model: "commensurateWageRate" },
      { title: 'Total hours worked for job described above', model: 'totalHours'  },
      { title: 'Does worker perform work for this employer at any other work site?', model: 'workAtOtherSite'  },
      {
          "className": 'edit-table-entry',
          "orderable": false,
          "data":null,
          "defaultContent": "<button class='green-button'>Edit</button>"
      },
      {
          "className": 'delete-table-entry',
          "orderable": false,
          "data":null,
          "defaultContent": "<button class='usa-button-secondary'>Delete</button>"
      }
    ]

    // multiple choice responses
    let questionKeys = ['WorkSiteType', 'PrimaryDisability'];
    responsesService.getQuestionResponses(questionKeys).then(responses => {
      $scope.responses = responses;
    });

    this.clearActiveWorker = function() {
      vm.activeWorker = {};
      vm.activeWorkerIndex = -1;
    };

    this.clearSaveStatus = function () {
      vm.saveEmployee.status = false;
      vm.saveEmployee.name = '';
    };

    this.addEmployee = function() {
      vm.newWorker = vm.activeWorker
      vm.newWorker.primaryDisabilityText = vm.getDisabilityDisplay(vm.newWorker);
      if (!vm.activeWorksite || isEmpty(vm.newWorker)) {
        return;
      }

      if (!vm.activeWorksite.employees) {
        vm.activeWorksite.employees = [];
      }

      if (vm.activeWorkerIndex > -1) {
        vm.activeWorksite.employees[vm.activeWorkerIndex] = vm.newWorker;
      } else {
        vm.activeWorksite.employees.push(vm.newWorker);
      }
      vm.saveEmployee.status = true;
      vm.saveEmployee.name = vm.activeWorker.name;

      vm.clearActiveWorker();


    };

    this.addAnotherEmployee = function() {
      vm.addEmployee();
      $document[0].getElementById('addEmployeesHeader').focus();
    };

    this.doneAddingEmployees = function($event) {
      vm.addEmployee();
      closeSlidingPanel(); 
      $event.preventDefault();
      vm.addingEmployee = false;
    };

    this.editEmployee = function(index, $event) {
      vm.clearSaveStatus();
      $event.preventDefault();
      if (vm.activeWorksite && vm.activeWorksite.employees.length > index) {
        vm.activeWorkerIndex = index;
        vm.activeWorker = merge({}, vm.activeWorksite.employees[index]);
        $('.cd-panel').addClass('is-visible');
        vm.addingEmployee = true;
      }
    };

    this.deleteEmployee = function(index, $event) {
      $event.preventDefault();
      if (vm.activeWorksite && vm.activeWorksite.employees.length > index) {
        vm.activeWorksite.employees.splice(index, 1);
      }
    };

    this.clearActiveWorkSite = function() {
      vm.activeWorksite = {};
      vm.activeWorksiteIndex = -1;
    };

    this.addWorkSite = function() {
      vm.addingWorkSite = true;
      vm.setActiveTab(1);
    };

    this.saveWorkSite = function() {
      if (!isEmpty(vm.activeWorksite)) {
        if (vm.activeWorksiteIndex > -1) {
          $scope.formData.workSites[vm.activeWorksiteIndex] = vm.activeWorksite;
        } else {
          $scope.formData.workSites.push(vm.activeWorksite);
        }
      }

      vm.clearActiveWorker();
      vm.clearActiveWorkSite();
      vm.addingWorkSite = false;
    };

    this.editWorkSite = function(index) {
      if ($scope.formData.workSites.length > index) {
        vm.activeWorksiteIndex = index;
        vm.activeWorksite = merge({}, $scope.formData.workSites[index]);
        vm.addingWorkSite = true;
        vm.setActiveTab(1);
      }
    };

    this.deleteWorkSite = function(index) {
      if ($scope.formData.workSites.length > index) {
        $scope.formData.workSites.splice(index, 1);
      }
    };

    this.setActiveTab = function(tab) {
      if (!vm.addingWorkSite || tab < 1 || tab > 2) {
        return;
      }

      vm.activeTab = tab;

      if (tab === 1 && vm.notInitialApp()) {
        navService.setNextQuery(
          { t: 2 },
          'Next: Add Employee(s)',
          'worksite_tab_box'
        );
        navService.setBackQuery({ doCancel: true }, 'Cancel');
      } else {
        navService.setNextQuery(
          { doSave: true },
          'Save Work Site & Employee(s)'
        );
        navService.setBackQuery({ doCancel: true }, 'Cancel');
      }
    };

    this.siteRowClicked = function(e) {
      let row = $(e.target).closest('.expanding-row');
      row.toggleClass('expanded');
      row.next().toggleClass('show');
    };

    this.getDisabilityDisplay = function(employee) {
      if (!employee) {
        return undefined;
      }

      var disability = find($scope.responses.PrimaryDisability, {
        id: employee.primaryDisabilityId
      });
      if (disability) {
        if (disability.otherValueKey) {
          return employee[disability.otherValueKey];
        } else {
          return disability.display;
        }
      }

      return undefined;
    };

    this.workerProductivityChanged = function(study) {
      if (!study && vm.activeWorker) {
        vm.activeWorker.productivityMeasure = undefined;
      }
    };

    // convenience methods to avoid lenghty template statements
    this.validateActiveWorksiteProperty = function(prop) {
      if (!prop || this.activeWorksiteIndex < 0) {
        return undefined;
      }

      return validationService.getValidationErrors(
        'workSites[' + this.activeWorksiteIndex + '].' + prop
      );
    };

    this.validateActiveWorkerProperty = function(prop) {
      if (!prop || this.activeWorksiteIndex < 0 || this.activeWorkerIndex < 0) {
        return undefined;
      }

      return validationService.getValidationErrors(
        'workSites[' +
          this.activeWorksiteIndex +
          '].employees[' +
          this.activeWorkerIndex +
          '].' +
          prop
      );
    };

    this.validateActiveWorksiteWorker = function(index) {
      if (this.activeWorksiteIndex < 0 || index < 0) {
        return undefined;
      }

      return validationService.getValidationErrors(
        'workSites[' + this.activeWorksiteIndex + '].employees[' + index + ']'
      );
    };

    this.notInitialApp = function() {
      return (
        $scope.formData.applicationTypeId !==
        _constants.responses.applicationType.initial
      );
    };

    $scope.$on('$routeUpdate', function() {
      query = $location.search();
      if (query.t) {
        vm.setActiveTab(query.t);
      }

      if (query.doSave) {
        vm.saveWorkSite();
        navService.clearQuery();
      }

      if (query.doCancel) {
        vm.clearActiveWorker();
        vm.clearActiveWorkSite();
        vm.addingWorkSite = false;
        navService.clearQuery();
      }
    });

    // Employee Data Example Views
    this.showExampleView = function(view) {
      vm.exampleView = view;
      $document[0].getElementById('exampleFirstFocus').focus();
    }

    // Show all help text
    this.toggleAllHelpText = function() {
      $scope.showAllHelp = !$scope.showAllHelp;
    }

    // Tab panel focus
    vm.tabPanelFocus = function(id) {
      if (id === 1) {
          $document[0].getElementById('worksitesTabPanel').focus();
      } else {
          $document[0].getElementById('employeesTabPanel').focus();
      }
    };

    // Sliding Panel
    var panelTrigger

    $('.cd-panel-trigger').on('click', function(event){
      panelTrigger = $(this);
      var target = $(this).attr('aria-controls');
      $(`#${target}`).addClass('is-visible');
      $(`#${target} .cd-panel-header h3`).focus();
      vm.clearActiveWorker();
      $('body').addClass('cd-panel-open'); 
      event.preventDefault();
    });

    // close the panel
    function closeSlidingPanel() {
      $('.cd-panel').removeClass('is-visible');
      $('body').removeClass('cd-panel-open');
      if (panelTrigger) {
        panelTrigger.focus();        
      }    
    }
    $(document).keydown(function(event) {
        // escape key
        if ($('.cd-panel').hasClass('is-visible') && event.keyCode === 27) {
          closeSlidingPanel();
        }
    });
    $('.cd-panel-close').on('click', function(event){
        closeSlidingPanel()
        event.preventDefault();
    });

    // trap keyboard access inside the panel
    $(".cd-panel .dol-last-focus").keydown(function(event){
      if (event.which === 9 && !event.shiftKey) {
        $(".cd-panel .dol-first-focus").focus();
        event.preventDefault();
      }
    });
    $(".cd-panel .dol-first-focus").keydown(function(event){
      if (event.shiftKey && event.which === 9) {
        $(".cd-panel .dol-last-focus").focus();
        event.preventDefault();
      }
    });

  });
};
