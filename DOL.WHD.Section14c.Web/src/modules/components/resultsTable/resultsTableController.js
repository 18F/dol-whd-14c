'use strict';
//import * as $ from 'jquery';
import 'datatables.net'
import 'datatables.net-buttons';
import 'datatables.net-buttons/js/buttons.html5.js';
import 'datatables.net-responsive';
import 'datatables.net-dt/css/jquery.datatables.css';
import 'datatables.net-buttons-dt/css/buttons.dataTables.css';
import 'datatables.net-responsive-dt/css/responsive.dataTables.css';

module.exports = function(ngModule) {
  ngModule.controller('resultsTableController', function(
    $scope
  ) {
    'ngInject';
    'use strict';
    $scope.data = [];
    $scope.initDatatable = function () {
      let exampleId = $('#example');
      $scope.tableWidget = exampleId.DataTable({
        data: $scope.data,
        dom:'Bfrtip',
        responsive: {
            details: {
                type: "column",
                target: 0,
                display: $.fn.dataTable.Responsive.display.childRow
            }
        },
        buttons: [
          'copy','csv'
         ],
        columns: $scope.columns,
        select: true,
        autoWidth: false,
        order: [[ 1, "desc" ]],
        columnDefs: [
            {
              className: 'control',
              orderable: false,
              targets:   0
          },
          { responsivePriority: 1, targets: 0 },
          { responsivePriority: 2, targets: 1 },
          { responsivePriority: 3, targets: 2 },
          { responsivePriority: 3, width: "10%", targets: $scope.columns.length -1 },
          { responsivePriority: 3, width: "10%", targets: $scope.columns.length -2 }
        ]
      });

      // $('#container').css( 'display', 'block' );
      // $scope.tableWidget.columns.adjust().draw();
    }

    $scope.refreshTable = function (data, columns) {
      columns = columns.map(function(element) {
        return element.model
      });
      if(data) {
        $scope.data = data.map(function(element){
          let arr = new Array(columns.length)

          for(var property in element) {
            if(element.hasOwnProperty(property)) {
              var index = columns.indexOf(property)
              if(index >=0 ) {
                arr[index] = element[property];
              }
              else if (property === "primaryDisabilityText") {
                index = columns.indexOf("primaryDisabilityId");
                arr[index] = element[property];
              }
            }
          }
          return arr;
        });
        if ($scope.tableWidget) {
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
