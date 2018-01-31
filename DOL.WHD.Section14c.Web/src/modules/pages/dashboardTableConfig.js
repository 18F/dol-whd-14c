var employeeColumns = [
  {
      "className": 'control',
      "orderable": false,
      "data":null,
      "defaultContent": ""
  },
  { data: 'ein', title: "EIN"},
  { data: 'employerName', title: "Employer" },
  { data: 'employerAddress',  title: "Address"},
  { data: 'createdAt', title: "Created At" },
  { data: 'lastModifiedAt',  title: "Last Modified"},
  { data: 'applicationStatus',  title: "Status"},
  { data: 'action', title: 'Action'}
];

var employeeColumnDefinitions = [
  {
      className: 'control',
      orderable: false,
      targets:   0
  },
  { responsivePriority: 1, targets: 1 },
  { responsivePriority: 1, targets: 6 },
  { responsivePriority: 1, targets: 3 },
  {
      className: 'action',
      orderable: false,
      responsivePriority: 1,
      targets: -1,
      render: function (data) {
          var button = "<button>" + data + "</button>"
          return button;
      },
  }
];

var order = [
  [2, "asc"],
  [5, "desc"]
];

module.exports = {
  employeeColumns: employeeColumns,
  employeeColumnDefinitions: employeeColumnDefinitions,
  order: order
}
