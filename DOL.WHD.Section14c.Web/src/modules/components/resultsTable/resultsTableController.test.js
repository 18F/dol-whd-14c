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

  it('refreshes table', function() {
    scope.columns = [ ];
    var controller = resultsTableController();
    scope.tableWidget = { destroy: function() { } };
    spyOn(controller, 'initDatatable');

    var columns = [
      { data: 'primaryDisabilityId' },
      { data: 'a' },
      { data: 'b' },
      { data: 'c' }
    ];

    var data = [
      { a: 10, b: 20, c: 30, primaryDisabilityText: 40 },
      { a: 11, b: 21, c: 31 },
      { a: 12, b: 22, c: 32 },
    ];

    controller.refreshTable(data, columns, "test");

    expect(scope.data).toEqual(data);
    expect(scope.tableWidget).toBe(null);
    setTimeout(function() {
      expect(controller.initDatatable).toHaveBeenCalledWith("test");
      expect(scope.tableWidget).not.toBe(null);
    }, 0);

    data = [{
      primaryDisabilityOther: true,
      primaryDisabilityText: "other"
    }];
    controller.refreshTable(data, columns, "test");
    //
    expect(scope.data[0].primaryDisabilityId).toEqual("other");
  });
});
