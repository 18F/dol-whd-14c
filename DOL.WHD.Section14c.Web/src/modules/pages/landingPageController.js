'use strict';

import * as tableConfig from './dashboardTableConfig';
module.exports = function(ngModule) {
  ngModule.controller('landingPageController', function(
    $scope,
    stateService,
    autoSaveService,
    apiService,
    dateFilter,
    $location
  ) {
    'ngInject';
    'use strict';
    
    $scope.currentApplication = undefined;
    $scope.submittedApplications = [];
    $scope.applicationLoadError = {
      status: false
    };
    $scope.clear = {
      status: "Inprogress",
      message: 'In Progress ...'
    };
    $scope.modalIsVisible = false;
    $scope.changePassword = function() {
      $location.path('/changePassword');
    };

    $scope.downloadApplication = function(index) {
       if($scope.submittedApplications[index].action === "Download"){
          stateService.downloadApplicationPdf($scope.submittedApplications[index].applicationId).then(function() {
            return;
          }).catch(function(error) {
            $scope.handleApplicationLoadError(error.data);
          });
       } else {
         $scope.handleApplicationLoadError('Invalid Status for Download');
       }
    };

    $scope.navToApplication = function () {
      if (stateService.ein) {
        autoSaveService.start();
        $location.path('/section/assurances');
      }
    };

    $scope.startNewApplication = function () {
      if (stateService.ein) {
        stateService.saveNewApplication().then(function(result) {
            stateService.applicationId = result.data.ApplicationId;
            $location.path('/section/assurances');
            autoSaveService.start();
          }).catch(function(error) {
            $scope.handleApplicationLoadError(error.data);
          });
      }
    };
    
    $scope.handleApplicationLoadError = function(message) {
      $scope.applicationLoadError.status = true;
      if(message) {
        $scope.applicationLoadError.message = message;
      }
    };

    $scope.loadUserInfo = function(employerId, ein, employerName) {
      stateService.employerId = employerId;
      stateService.ein = ein;
      stateService.employerName = employerName;
      return;
    }

    $scope.setClearStatus = function (status, message) {
      $scope.clear.status = status;
      $scope.clear.message = message;
    };

    $scope.showClearApplicationConfirmationModal = function () {
      $scope.modalIsVisible = true;
      $scope.setClearStatus('Initialize', 'Are you sure you want to clear all data?');
    };

    $scope.hideClearApplicationConfirmationModal = function() {
      $scope.modalIsVisible = false;
    }

    $scope.clearApplication = function(){
      $scope.setClearStatus('Clearing', 'Attempting to clear application.');
      apiService.clearApplication(stateService.access_token, stateService.applicationId)
      .then(
        function() {
          // Reload Page
          $scope.modalIsVisible = false;
          stateService.resetFormData();
        },
        function(e) {
          apiService.parseErrors(e.data);
          $scope.setClearStatus('Failure', 'Failed to clear application.');
        }
      );
    }

    $scope.init = function () {
      apiService.userInfo(stateService.access_token).then(function(result) {
        result.data.organizations.forEach(function(element) {
          // Milestone 1: User can only have one employer (id, ein, and legalName will be consistent for every memeber)
          $scope.loadUserInfo(element.employer.id, element.employer.ein, element.employer.legalName);
          var organization = {
            ein: element.employer.ein,
            employerId: element.employer.id,
            employerName: element.employer.legalName,
            createdAt: dateFilter(element.createdAt) + " at " + new Date(element.createdAt).toLocaleTimeString(),
            lastModifiedAt: dateFilter(element.lastModifiedAt) + " at " + new Date(element.lastModifiedAt).toLocaleTimeString(),
            employerAddress: element.employer.physicalAddress.streetAddress + " " + element.employer.physicalAddress.city + ", " + element.employer.physicalAddress.state + " " + element.employer.physicalAddress.zipCode
          }
          if(element.applicationId && element.applicationStatus) {
            organization.applicationId = element.applicationId;
            organization.applicationStatus = element.applicationStatus.name;
             if(element.applicationStatus.name === "Submitted") {
              organization.action = "Download";
              $scope.submittedApplications.push(organization);
            }
          }
          if(organization.action != "Download") {
            // Milestone 1: User can only have one in progress application at a given time
            $scope.currentApplication = organization;
          }
        });
        $scope.initDatatable();
      });
    }

    $scope.initDatatable = function () {
      $scope.tableWidget = $('#EmployerTable').DataTable({
        data: $scope.submittedApplications,
        language: {
          emptyTable: "You do not have any submitted applications. Once you submit an application, you can download the application PDF here."
        },
        responsive: {
            details: {
                type: "column",
                target: 0,
                display: $.fn.dataTable.Responsive.display.childRow
            }
        },
        ordering: true,
        autoWidth: false,
        order: tableConfig.order,
        columns: tableConfig.employeeColumns,
        columnDefs: tableConfig.employeeColumnDefinitions,
      });

      setTimeout(() => $scope.tableWidget.columns.adjust().draw(), 0 );
    }


    $scope.refreshTable = function () {
      if ($scope.tableWidget) {
        $scope.tableWidget.destroy()
        $scope.tableWidget=null
      }
      setTimeout(() => $scope.initDatatable(),0)
    }

    $('#EmployerTable').on('click', '.action', function ($event) {
        $event.preventDefault();
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        $scope.downloadApplication(row[0][0]);
    });

    $scope.init();

  });
};
