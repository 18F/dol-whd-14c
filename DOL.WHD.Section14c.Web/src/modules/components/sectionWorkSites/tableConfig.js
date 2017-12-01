var employeeColumns = [
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
      "defaultContent": "<button class='green-button'>Edit</button>"
  },
  {
      "className": 'delete-table-entry',
      "orderable": false,
      "data":null,
      "defaultContent": "<button class='usa-button-secondary'>Delete</button>"
  }
]

var employeeColumnDefinitions = [
  {
      className: 'control',
      orderable: false,
      targets:   0
  },
  { responsivePriority: 1, targets: 0 },
  { responsivePriority: 2, targets: 1 },
  { responsivePriority: 3, targets: 2 },
  { responsivePriority: 3, width: "10%", targets: employeeColumns.length -1 },
  { responsivePriority: 3, width: "10%", targets: employeeColumns.length -2 }
]

var workSiteColumns = [
  {
      "className": '',
      "orderable": false,
      "data":null,
      "defaultContent": ""
  },
  { title: 'Name', model: 'name' },
  { title: 'Number of Workers', model: 'numEmployees'  },
  { title: 'Establishing a Minimum Wage for Contractors', model: 'federalContractWorkPerformed'  },
  { title: 'Service Contract Act', model: 'sca'  },
  { title: 'Work Site Type', model: 'workSiteTypeId'  },
  { title: 'Address', model: 'address'  },
  {
      "className": 'edit-table-entry',
      "orderable": false,
      "data":null,
      "defaultContent": "<button class='green-button'>Edit</button>"
  },
  {
      "className": 'delete-table-entry',
      "orderable": false,
      "data":null,
      "defaultContent": "<button class='usa-button-secondary'>Delete</button>"
  }
];
var workSiteColumnDefinitions= [
  {
      className: 'control',
      orderable: false,
      targets:   0
  },
  { responsivePriority: 1, targets: 0 },
  { responsivePriority: 2, targets: 1 },
  { responsivePriority: 3, targets: 2 },
];
module.exports = {
  employeeColumns: employeeColumns,
  employeeColumnDefinitions: employeeColumnDefinitions,
  workSiteColumns: workSiteColumns,
  workSiteColumnDefinitions: workSiteColumnDefinitions
}
