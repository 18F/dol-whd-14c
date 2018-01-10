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

    $scope.onApplicationClick = function(index) {
      stateService.employerId = $scope.organizations[index].employer.id;
      stateService.applicationId = $scope.organizations[index].applicationId;
      stateService.ein = $scope.organizations[index].ein;
      if($scope.organizations[index].applicationStatus.name === "New") {
         stateService.saveNewApplication().then(
          function() {
            // start auto-save
            if (stateService.ein) {
              autoSaveService.start();
              $location.path('/section/assurances');
            }

          },
          function() {
          }
        );
       } 
       else if($scope.organizations[index].applicationStatus.name === "Submitted"){
        stateService.downloadApplicationPdf();
       }
       else {
        stateService.loadSavedApplication().then(
          function() {
            // start auto-save
            if (stateService.ein) {
              autoSaveService.start();
              $location.path('/section/assurances');
            }

          },
          function() {
          }
        );
       }

      $location.path('/section/assurances');
    };

    $scope.navToEmployerRegistration = function ()  {
      $location.path('/employerRegistration');
    };

    $scope.init = function () {
      apiService.userInfo(stateService.access_token).then(function(result) {
        $scope.organizations = result.data.organizations;
        $scope.applicationList = [];
        result.data.organizations.forEach(function(element) {
          var organization = {
            employerId: element.employer.ein,
            employerName: element.employer.legalName,
            createdAt: dateFilter(element.createdAt) + " at " + new Date(element.createdAt).toLocaleTimeString(),
            lastModifiedAt: dateFilter(element.lastModifiedAt) + " at " + new Date(element.lastModifiedAt).toLocaleTimeString(),
            applicationStatus: element.applicationStatus.name
          }
          
          $scope.applicationList.push(organization);
        });

        $scope.tableWidget = $('#EmployerTable').DataTable({
          data: $scope.applicationList,
          columns: [
            { data: 'employerId', title: "EIN"},
            { data: 'employerName', title: "Employer" },
            { data: 'createdAt', title: "Created At" },
            { data: 'lastModifiedAt',  title: "Last Modified"},
            { data: 'applicationStatus',  title: "Status"},
          ],
          columnDefs: [
            {
                className: 'control',
                orderable: false,
                targets:   0
            },
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: 1 },
            { responsivePriority: 3, targets: 2 }
          ]
        });
      })
    }

    $scope.init();
    $('#EmployerTable').on('click', 'td', function ($event) {
        $event.preventDefault();
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        $scope.onApplicationClick(row[0][0]);
    });

  });
};
