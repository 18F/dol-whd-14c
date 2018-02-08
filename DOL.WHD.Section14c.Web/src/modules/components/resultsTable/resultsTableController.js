'use strict';
import 'datatables.net'
import 'datatables.net-buttons';
import 'datatables.net-buttons/js/buttons.html5.js';
import 'datatables.net-responsive';
import 'datatables.net-dt/css/jquery.dataTables.css';
import 'datatables.net-buttons-dt/css/buttons.dataTables.css';
import 'datatables.net-responsive-dt/css/responsive.dataTables.css';

module.exports = function(ngModule) {
  ngModule.controller('resultsTableController', function(
    $scope,
    $attrs
  ) {

    'ngInject';
    'use strict';

    $scope.vm = this;
    $scope.data = $scope.results;
    this.initDatatable = function (id) {
      let dt = $("#" + id).children("table");
      $scope.tableWidget = dt.DataTable({
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
          { extend: 'copy', text: 'Copy<span class="hide"> table data to clipboard</span>' },
          { extend: 'csv', text: '<span class="hide">Generate a </span>CSV<span class="hide"> file from the table data</span>' }
         ],
        'language': {
          'search': 'Search<span class="hide"> Table Data</span>'
        },
        columns: $scope.columns,
        select: true,
        autoWidth: false,
        order: [[ 1, "desc" ]],
        columnDefs: $scope.definitions
      });
      $.fn.dataTable.ext.errMode = 'none';
    }

    this.refreshTable = function (data, columns, id) {
      if(data) {
        data.forEach(function(element) {
          if(element.primaryDisabilityOther) {
            element.primaryDisabilityId = element.primaryDisabilityText;
          }
        });
        $scope.data = data;
        if ($scope.tableWidget) {
          $scope.tableWidget.destroy()
          $scope.tableWidget=null
        }
       setTimeout(() => this.initDatatable(id),0)
      }
    }

    //this.initDatatable();
    //this.refreshTable(null, $scope.columns);

    $('#' + $attrs.id).children("table").on('click', 'td.edit-table-entry', function ($event) {
        $event.preventDefault();
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        $scope.edit()(row[0][0], $event);
    });

    $('#' + $attrs.id).children("table").on('click', 'td.delete-table-entry', function ($event) {
        $event.preventDefault();
        var tr = $(this).closest('tr');
        var row = $scope.tableWidget.row( tr );
        $scope.delete()(row[0][0], $event);
    });
  });
};
