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

    $scope.changePassword = function() {
      $location.path('/changePassword');
    };
    $scope.applicationLoadError = {
      status: false
    };

    $scope.onApplicationClick = function(index) {
      $scope.loadUserInfo(index);
      if($scope.organizations[index].applicationStatus.name === "New") {

         stateService.saveNewApplication().then(function() {
            $scope.navToApplication();
          }).catch(function(error) {
            $scope.handleApplicationLoadError(error.data);
          });
       }
       else if($scope.organizations[index].applicationStatus.name === "Submitted"){
          stateService.downloadApplicationPdf();
       }
       else if ($scope.organizations[index].applicationStatus.name === "InProgress") {
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

    $scope.handleApplicationLoadError = function(message) {
      $scope.applicationLoadError.status = true;
      if(message) {
        $scope.applicationLoadError.message = message;
      }
    };

    $scope.loadUserInfo = function(index) {
      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;
      return;
    }

    $scope.init = function () {
      apiService.userInfo(stateService.access_token).then(function(result) {
        $scope.organizations = result.data.organizations;
        $scope.applicationList = [];
        result.data.organizations.forEach(function(element) {
          var organization = {
            applicationId: element.applicationId,
            employerId: element.employer.ein,
            employerName: element.employer.legalName,
            createdAt: dateFilter(element.createdAt) + " at " + new Date(element.createdAt).toLocaleTimeString(),
            lastModifiedAt: dateFilter(element.lastModifiedAt) + " at " + new Date(element.lastModifiedAt).toLocaleTimeString(),
            applicationStatus: element.applicationStatus.name,
            employerAddress: element.employer.physicalAddress.streetAddress + " " + element.employer.physicalAddress.city + ", " + element.employer.physicalAddress.state + " " + element.employer.physicalAddress.zipCode
          }

          if(element.applicationStatus.name === "New") {
            organization.action = "Start"
          }
          else if (element.applicationStatus.name === "InProgress") {
            organization.action = "Continue"
          } else {
            organization.action = "Download"
          }

          $scope.applicationList.push(organization);
        });
        $scope.initDatatable();
      });
    }

    $scope.initDatatable = function () {
      $scope.tableWidget = $('#EmployerTable').DataTable({
        data: $scope.applicationList,
        responsive: {
            details: {
                type: "column",
                target: 0,
                display: $.fn.dataTable.Responsive.display.childRow
            }
        },
        ordering: true,
        order: tableConfig.order,
        columns: tableConfig.employeeColumns,
        columnDefs: tableConfig.employeeColumnDefinitions,
      });
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
