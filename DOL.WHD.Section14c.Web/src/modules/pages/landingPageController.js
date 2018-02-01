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

    $scope.changePassword = function() {
      $location.path('/changePassword');
    };
    $scope.applicationLoadError = {
      status: false
    };

    $scope.onApplicationClick = function(index) {
      $scope.loadUserInfo(index);
      if($scope.submittedApplications[index].applicationStatus.name === "New") {

         stateService.saveNewApplication().then(function() {
            $scope.navToApplication();
          }).catch(function(error) {
            $scope.handleApplicationLoadError(error.data);
          });
       }
       else if($scope.submittedApplications[index].applicationStatus.name === "Submitted"){
          stateService.downloadApplicationPdf().then(function() {
            return;
          }).catch(function(error) {
            $scope.handleApplicationLoadError(error.data);
          });
       }
       else if ($scope.submittedApplications[index].applicationStatus.name === "InProgress") {
        stateService.loadSavedApplication().then(function() {
            $scope.navToApplication();
          }).catch(function(error) {
            $scope.handleApplicationLoadError(error.data);
          });
      } else {
        $scope.handleApplicationLoadError('Invalid Application State');
      }
    };

    $scope.navToEmployerRegistration = function ()  {
      $location.path('/employerRegistration');
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

    $scope.loadUserInfo = function(index) {
      stateService.employerId = $scope.submittedApplications[index].employer.id;
      stateService.applicationId = $scope.submittedApplications[index].applicationId;
      stateService.ein = $scope.submittedApplications[index].ein;
      stateService.employerName = $scope.submittedApplications[index].employer.legalName;
      return;
    }

    $scope.init = function () {
      apiService.userInfo(stateService.access_token).then(function(result) {
        $scope.submittedApplications = [];
        result.data.organizations.forEach(function(element) {
          stateService.ein = element.employer.ein;
          stateService.employerId = element.employer.id;
          stateService.employerName = element.employer.legalName;

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
            } else {
              $scope.currentApplication = organization;
            }
          } else {
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
        $scope.onApplicationClick(row[0][0]);
    });

    $scope.init();

  });
};
