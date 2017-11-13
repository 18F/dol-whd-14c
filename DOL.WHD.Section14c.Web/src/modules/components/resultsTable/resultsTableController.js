'use strict';
//import * as $ from 'jquery';
import 'datatables.net'
import 'datatables.net-buttons';
import 'datatables.net-buttons/js/buttons.html5.js';
import 'datatables.net-responsive';
import 'datatables.net-dt/css/jquery.datatables.css';
import 'datatables.net-buttons-dt/css/buttons.dataTables.css';
// import 'datatables.net-responsive-dt/css/responsive.dataTables.css';

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
    $scope.data = [];
    $scope.initDatatable = function () {
      let exampleId = $('#example');
      $scope.tableWidget = exampleId.DataTable({
        data: $scope.data,
        dom:'Bfrtip',
        //responsive: true,
        buttons: [
          'copy', 'excel', 'pdf', 'csv'
         ],
        columns: $scope.columns,
        select: true,
        order: [[ 2, "desc" ]]
      });
    }


    $scope.refreshTable = function (data, columns) {
      columns = columns.map(function(element) {
        return element.model
      });
      if(data) {
        $scope.data = data.map(function(element){
          let arr = new Array(columns.length)
          arr.unshift("Delete")
          arr.unshift("Edit");
          for(var property in element) {
            if(element.hasOwnProperty(property)) {
              var index = columns.indexOf(property)
              if(index >=0 ) {
                arr[index] = element[property]
                arr.push(element[property]);
              }
            }
          }
          return arr;
        });
        if (this.tableWidget) {
          $scope.tableWidget.destroy()
          $scope.tableWidget=null
        }
       setTimeout(() => $scope.initDatatable(),0)
      }
    }

    $scope.initDatatable();

    $('#example tbody').on('click', 'td.edit-table-entry', function ($event) {
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        $scope.$parent.vm.editEmployee(row[0][0], $event);
    } );

    $('#example tbody').on('click', 'td.delete-table-entry', function ($event) {
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        $scope.$parent.vm.deleteEmployee(row[0][0], $event);
    } );
  });
};
