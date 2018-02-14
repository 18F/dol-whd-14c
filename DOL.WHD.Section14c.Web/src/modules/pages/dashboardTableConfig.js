var applicationColumns = [
  {
      "className": 'control',
      "orderable": false,
      "data":null,
      "defaultContent": ""
  },
  { data: 'employerName', title: "Employer" },
  { data: 'ein', title: "EIN"},
  { data: 'employerAddress',  title: "Address"},
  { data: 'lastModifiedAt',  title: "Date Submitted"},
  { data: 'applicationStatus',  title: "Status"},
  { data: 'action', title: 'PDF Version'}
];

var applicationColumnDefinitions = [
  {
      className: 'control',
      orderable: false,
      targets:   0
  },
  { responsivePriority: 1, targets: 1 },
  { responsivePriority: 2, targets: -3 },
  {
      className: 'action',
      orderable: false,
      responsivePriority: 3,
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
  applicationColumns: applicationColumns,
  applicationColumnDefinitions: applicationColumnDefinitions,
  order: order
}
