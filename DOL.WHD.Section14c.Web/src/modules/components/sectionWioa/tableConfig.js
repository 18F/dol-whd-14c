var wioaColumns = [
  {
      "className": '',
      "orderable": false,
      "data":null,
      "defaultContent": ""
  },
  { title: 'Name of Worker', model: 'fullName' },
  { title: 'Response', model: 'WIOAWorkerVerified'  },
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

var wioaColumnDefinitions = [
  {
      className: 'control',
      orderable: false,
      targets:   0
  },
  { responsivePriority: 1, targets: 0 },
  { responsivePriority: 2, targets: 1 },
  { responsivePriority: 3, targets: 2 },
  { responsivePriority: 3, width: "10%", targets: wioaColumns.length -1 },
  { responsivePriority: 3, width: "10%", targets: wioaColumns.length -2 }
]

module.exports = {
  wioaColumns: wioaColumns,
  wioaColumnDefinitions: wioaColumnDefinitions
}
