'use strict';
//import * as $ from 'jquery';
import 'datatables.net'

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
        columns: $scope.columns
      });
    }


    $scope.refreshTable = function (data) {
      const results = data;
      if(data) {
        console.log(data);
        $scope.data = results.map(function(element){
          let arr = [];
          for(var property in element) {
            if(element.hasOwnProperty(property)) {
              if(property!=="$$hashKey"){
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
       // this.needupdate = false;
       // this.needupdateChange.emit(this.needupdate);
      }

    }

        $scope.initDatatable();
    // export class WageDataComponent implements OnInit {
    //   public tableWidget: any;
    //   public _data = [];
    //   @Input() data: any;
    //   @Input() responses: any;
    //   @Input() needupdate: boolean;
    //   @Output() needupdateChange: EventEmitter<boolean> = new EventEmitter<boolean>();
    //   ngAfterViewInit() {
    //     this.initDatatable()
    //   }
    //
    //   getDisabilityDisplay (employee: any) {
    //     //console.log(this.disabilitydata)
    //   }
    //
    //
    //   ngDoCheck () {
    //     if(this.needupdate) {
    //       console.log('update needed', this.needupdate)
    //       this.refreshTable(this.data);
    //     }
    //   }
    //
    //   private initDatatable(): void {
    //
    //   }
    //
    //   private refreshTable(data: Array<any>): void {
    //     let disabilityData = this.responses.PrimaryDisability;
    //     this._data = data.map(function(element) {
    //       let disability = disabilityData.filter(function(disability){
    //         return disability.id === element.primaryDisabilityId;
    //       });
    //       if(disability[0]){
    //         element.primaryDisabilityId = disability[0].display;
    //       }
    //       let arr = [];
    //       for (var property in element) {
    //         if (element.hasOwnProperty(property)) {
    //
    //           if(property!=="$$hashKey"){
    //             arr.push(element[property]);
    //           }
    //         }
    //       }
    //       return arr;
    //     });
    //
    //     if (this.tableWidget) {
    //      this.tableWidget.destroy()
    //      this.tableWidget=null
    //    }
    //    setTimeout(() => this.initDatatable(),0)
    //    this.needupdate = false;
    //    this.needupdateChange.emit(this.needupdate);
    //   }
    //
    //
    //
    //   ngOnInit(){
    //     //console.log(this.data);
    //     console.log(this.responses)
    //
    //   }
  });
};
