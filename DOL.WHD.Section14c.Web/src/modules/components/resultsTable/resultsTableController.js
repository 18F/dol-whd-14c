'use strict';
//import * as $ from 'jquery';
import 'datatables.net'
import 'datatables.net-dt/css/jquery.datatables.css';

module.exports = function(ngModule) {
  ngModule.controller('resultsTableController', function(
    $scope,
    stateService,
    apiService,
    responsesService,
    validationService,
    _constants
  ) {
    'ngInject';
    'use strict';
    console.log($scope.$parent)
    $scope.data = [];
    $scope.initDatatable = function () {
      let exampleId = $('#example');
      $scope.tableWidget = exampleId.DataTable({
        data: $scope.data,
        columns: $scope.columns,
        select: true,
        order: [[ 2, "desc" ]]
      });
    }


    $scope.refreshTable = function (data) {
      if(data) {
        $scope.data = data.map(function(element){
          let arr = [];
          for(var property in element) {
            if(element.hasOwnProperty(property)) {
              if(property!=="$$hashKey"){
                arr.push(element[property]);
              }
            }
          }
          arr.unshift("Edit");
          return arr;
        });
        if (this.tableWidget) {
          $scope.tableWidget.destroy()
          $scope.tableWidget=null
        }
       setTimeout(() => $scope.initDatatable(),0)
       // this.needupdate = false;
       // this.needupdateChange.emit(this.needupdate);
      }

    }

    $scope.initDatatable();

    $('#example tbody').on('click', 'td.edit-table-entry', function () {
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        console.log($scope.$parent)
        $scope.$parent.vm.editEmployee(0);
    } );

    $('#example tbody').on('click', 'td.delete-table-entry', function () {
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        console.log($scope.$parent)
        $scope.$parent.vm.deleteEmployee(0);
    } );
  });
};
