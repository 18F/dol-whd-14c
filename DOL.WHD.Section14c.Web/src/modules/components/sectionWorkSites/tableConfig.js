var employeeColumns = [
  {
      className: '',
      orderable: false,
      data:null,
      defaultContent: ''
  },
  { title: 'Name', data: 'name' },
  { title: 'Type of work performed', data: 'workType'  },
  { title: 'Primary disability', data: 'primaryDisabilityId'},
  { title: 'How many jobs did this worker perform at this work site?', data: 'numJobs'  },
  { title: 'Average # of hours worked per week on all jobs at this work site', data: 'avgWeeklyHours'  },
  { title: 'Average earnings per hour for all jobs at this work site', data: 'avgHourlyEarnings'  },
  { title: 'Prevailing wage rate for job described above', data: 'prevailingWage'  },
  { title: 'Productivity measure/rating for job described above', data: 'hasProductivityMeasure', render: function(data){
      if(data === null) {
        return 'n/a - piece rate'
      }
    }
  },
  { title: 'Commensurate wage rate/average earnings per hour for job described above', data: 'commensurateWageRate' },
  { title: 'Total hours worked for job described above', data: 'totalHours'  },
  { title: 'Does worker perform work for this employer at any other work site?', data: 'workAtOtherSite'  },
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
  {targets: 3, render: function(data) {
      if(data === 31) {
        return 'Intellectual/Developmental Disability (IDD)'
      }
      if(data === 32) {
        return 'Psychiatric Disability (PD)';
      }
      if(data === 33) {
        return 'Visual Impairment (VI)';
      }
      if(data === 34) {
        return 'Hearing Impairment (HI)';
      }
      if(data === 35) {
        return 'Substance Abuse (SA)';
      }
      if(data === 36) {
        return 'Neuromuscular Disability (NM)';
      }
      if(data === 37) {
        return 'Age Related Disability (AR)';
      }
      return data;
    }
  },
  { targets: 11, render: function(data) { return (data ? 'yes' : 'no')}},
  { responsivePriority: 3, width: "10%", targets: employeeColumns.length -1 },
  { responsivePriority: 3, width: "10%", targets: employeeColumns.length -2 }
]

var workSiteColumns = [
  {
      className: '',
      orderable: false,
      data:null,
      defaultContent: ''
  },
  { title: 'Name', data: 'name' },
  { title: 'Number of Workers', data: 'numEmployees'  },
  { title: 'Establishing a Minimum Wage for Contractors', data: 'federalContractWorkPerformed'  },
  { title: 'Service Contract Act', data: 'sca'  },
  { title: 'Work Site Type', data: 'workSiteTypeId'  },
  { title: 'Address', data: 'address'  },
  {
      className: 'edit-table-entry',
      orderable: false,
      data:null,
      defaultContent: "<button class='green-button'>Edit</button>"
  },
  {
      className: 'delete-table-entry',
      orderable: false,
      data:null,
      defaultContent: "<button class='usa-button-secondary'>Delete</button>"
  }
];
var workSiteColumnDefinitions= [
  {
      className: 'control',
      orderable: false,
      targets:   0,
      responsivePriority: 1
  },
  { responsivePriority: 2, targets: 2 },
  { responsivePriority: 2, targets: 6 , render: function(data) {
      if(data) {
        return data.streetAddress + " " + data.city + ", " + data.state;
      }
    }
  },
  { responsivePriority: 2, targets: 1 },
  { targets: [3,4], render: function(data) { return (data ? 'yes' : 'no')}},
  { targets: 5, render: function(data) {
      if(data === 27) {
        return 'Main Establishment (ME)'
      }
      if(data === 28) {
        return 'Branch Establishment (BR)'
      }
      if(data === 29) {
        return 'Off-site Work Location (OL)'
      }
      if(data === 30) {
        return 'School Work Experience Program (SWEP)'
      }
    }
  },
  { responsivePriority: 1, width: '10%', targets: workSiteColumns.length -1 },
  { responsivePriority: 1, width: '10%', targets: workSiteColumns.length -2 }
];
module.exports = {
  employeeColumns: employeeColumns,
  employeeColumnDefinitions: employeeColumnDefinitions,
  workSiteColumns: workSiteColumns,
  workSiteColumnDefinitions: workSiteColumnDefinitions
}
