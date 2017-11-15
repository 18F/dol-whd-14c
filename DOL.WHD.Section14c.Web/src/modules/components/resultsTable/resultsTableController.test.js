describe('resultsTableController', function() {
var resultsTableController, scope
  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();
      scope.responses = {
        PrimaryDisability: [
          {
            id: 31,
            display: 'Intellectual/Developmental Disability (IDD)',
            otherValueKey: null
          },
          {
            id: 38,
            display: 'Other, please specify:',
            otherValueKey: 'primaryDisabilityOther'
          }
        ]
      };

      scope.columns = [
        {
            "className": '',
            "orderable": false,
            "data":null,
            "defaultContent": ""
        },
        { title: 'Name', model: 'name' },
        { title: 'Type of work performed', model: 'workType'  },
        { title: 'Primary disability', model: 'primaryDisabilityId'  },
        { title: 'How many jobs did this worker perform at this work site?', model: 'numJobs'  },
        { title: 'Average # of hours worked per week on all jobs at this work site', model: 'avgWeeklyHours'  },
        { title: 'Average earnings per hour for all jobs at this work site', model: 'avgHourlyEarnings'  },
        { title: 'Prevailing wage rate for job described above', model: 'prevailingWage'  },
        { title: 'Productivity measure/rating for job described above', model: 'hasProductivityMeasure'  },
        { title: 'Commensurate wage rate/average earnings per hour for job described above', model: "commensurateWageRate" },
        { title: 'Total hours worked for job described above', model: 'totalHours'  },
        { title: 'Does worker perform work for this employer at any other work site?', model: 'workAtOtherSite'  },
        {
            "className": 'edit-table-entry',
            "orderable": false,
            "data":null,
            "defaultContent": "<button class='usa-button-secondary'>Edit</button>"
        },
        {
            "className": 'delete-table-entry',
            "orderable": false,
            "data":null,
            "defaultContent": "<button class='usa-button-secondary'>Delete</button>"
        }
      ]

      scope.results =  [{
        avgHourlyEarnings:0.1,
        avgWeeklyHours:0.1,
        commensurateWageRate:0.1,
        hasProductivityMeasure:true,
        name:"zxcv",
        numJobs:4,
        prevailingWage:0.1,
        primaryDisabilityId:34,
        primaryDisabilityText:"Hearing Impairment (HI)",
        productivityMeasure:0.1,
        totalHours:0.1,
        workAtOtherSite:false,
        workType:"xcv"
      }];

      resultsTableController = function() {
        return $controller('resultsTableController', {
          $scope: scope
        });
      };
    })
  );

  it('sectionWorkSitesController has refreshTable function', function() {
    var controller = resultsTableController();
    spyOn(controller, "refreshTable");
    controller.refreshTable(scope.results, scope.columns);
    expect(controller.refreshTable).toHaveBeenCalled();
  });

  it('sectionWorkSitesController has initDatatable function', function() {
    var controller = resultsTableController();
    spyOn(controller, "initDatatable");
    controller.initDatatable(scope.results, scope.columns);
    expect(controller.initDatatable).toHaveBeenCalled();
  });
});
