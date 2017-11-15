describe('resultsTable', function() {
  var compileElement, template, $controller, $rootScope, $compiler, controller, scope, compiler
  var element = angular.element('<results-table results="results" columns="columns"></results-table>');
  beforeEach(module('14c'));
  beforeEach(function() {
    inject(function(_$rootScope_, _$compile_, _$controller_) {
      $compile = _$compile_;
      $controller = _$controller_;
      $rootScope = _$rootScope_;
      scope = $rootScope.$new();
      //
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

      scope.results = [];


        template = $compile(element)(scope);
        scope.$digest();
        controller = element.controller;
    });


  });
  it('invoke directive', function() {
    expect(element).toBeDefined();
  });

  it('invoke directive', function() {
    expect(element).toBeDefined();
  });

  it('columns should exist on scope directive', function() {
    expect(scope.columns).toBeDefined();
  });

  it('refresh datatable', inject(function() {
    var ctrl = element.controller('resultsTable');
    console.log(ctrl.refreshTable)
    spyOn(ctrl, "refreshTable")
    expect(scope.refreshTable).toHaveBeenCalled();
  }));


});
