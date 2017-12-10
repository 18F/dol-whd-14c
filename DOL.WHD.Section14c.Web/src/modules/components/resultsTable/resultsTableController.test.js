describe('resultsTableController', function() {
var resultsTableController, scope, attrs
  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller) {
      scope = $rootScope.$new();
      attrs = {
        id: "test"
      };

      resultsTableController = function() {
        return $controller('resultsTableController', {
          $scope: scope,
          $attrs: attrs
        });
      };
    })
  );

  it('initDatatable', function() {
    scope.columns = [ ];
    scope.tableWidget = undefined;

    var controller = resultsTableController();
    controller.initDatatable("test");

    // Ideally we could mock the call to jQuery.DataTable and
    // just verify that it was called, but since we can't do
    // that...  just make sure we got a widget...

    expect(scope.tableWidget).toBeDefined();
  });

  it('refreshTable', function() {
    scope.columns = [ ];
    var controller = resultsTableController();
    scope.tableWidget = { destroy: function() { } };
    spyOn(controller, 'initDatatable');

    var columns = [
      { model: 'primaryDisabilityId' },
      { model: 'a' },
      { model: 'b' },
      { model: 'c' }
    ];

    var data = [
      { a: 10, b: 20, c: 30, primaryDisabilityText: 40 },
      { a: 11, b: 21, c: 31 },
      { a: 12, b: 22, c: 32 },
    ];

    controller.refreshTable(data, columns, "test");

    expect(scope.data).toEqual([
      [ 40, 10, 20, 30 ],
      [ undefined, 11, 21, 31 ],
      [ undefined, 12, 22, 32 ]
    ]);
    expect(scope.tableWidget).toBe(null);
    setTimeout(function() {
      expect(controller.initDatatable).toHaveBeenCalled();
    }, 0);
  });
});
