'use strict';


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
      $scope.loadUserInfo($index);
      if($scope.organizations[index].applicationStatus.name === "New") {
         stateService.saveNewApplication().then(
          function() {
            if (stateService.ein) {
              autoSaveService.start();
              $location.path('/section/assurances');
            }
          },
          function(error) {
            $scope.handleApplicationLoadError(error.data);
          }
        );
       }
       else if($scope.organizations[index].applicationStatus.name === "Submitted"){
        stateService.downloadApplicationPdf();
       }
       else if ($scope.organizations[index].applicationStatus.name === "InProgress") {
        stateService.loadSavedApplication().then(
          function() {
            // start auto-save
            if (stateService.ein) {
              autoSaveService.start();
              $location.path('/section/assurances');
            }
          },
          function(error) {
            $scope.handleApplicationLoadError(error.data);
          }
        );
      } else {
        $scope.handleError('Invalid Application State');
      }
    };

    $scope.navToEmployerRegistration = function ()  {
      $location.path('/employerRegistration');
    };

    $scope.handleApplicationLoadError = function(message) {
      $scope.applicationLoadError.status = true;
      $scope.applicationLoadError.message = message;
    };

    $scope.loadUserInfo = function(index) {
      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;
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
      })
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
        order: [
          [2, "asc"],
          [5, "desc"]
        ],
        columns: [
          {
              "className": 'control',
              "orderable": false,
              "data":null,
              "defaultContent": ""
          },
          { data: 'employerId', title: "EIN"},
          { data: 'employerName', title: "Employer" },
          { data: 'employerAddress',  title: "Address"},
          { data: 'createdAt', title: "Created At" },
          { data: 'lastModifiedAt',  title: "Last Modified"},
          { data: 'applicationStatus',  title: "Status"},
          { data: 'action', title: 'Action'}
        ],
        columnDefs: [
          {
              className: 'control',
              orderable: false,
              targets:   0
          },
          { responsivePriority: 1, targets: 1 },
          { responsivePriority: 1, targets: 6 },
          { responsivePriority: 1, targets: 3 },
          {
              className: 'action',
              orderable: false,
              responsivePriority: 1,
              targets: -1,
              render: function (data) {
                  var button = "<button>" + data + "</button>"
                  return button;
              },
          }
        ]
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
