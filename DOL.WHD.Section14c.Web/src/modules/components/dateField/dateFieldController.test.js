describe('dateFieldController', function() {
  var scope, momentMock, dateFieldController;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, moment) {
      scope = $rootScope.$new();
      momentMock = moment;

      dateFieldController = function() {
        return $controller('dateFieldController', {
          $scope: scope,
          moment: momentMock
        });
      };
    })
  );

  it('invoke controller', function() {
    scope.dateVal = '2000-01-01';
    var controller = dateFieldController();
    scope.vm = controller;
    scope.$digest();

    expect(controller.year).toBe(2000);
    expect(controller.day).toBe(1);
    expect(controller.month).toBe(1);
  });

  it('valid date change', function() {
    var controller = dateFieldController();
    scope.vm = controller;
    controller.year = '2000';
    controller.month = '01';
    controller.day = '01';
    scope.$digest();

    controller.year = '2001';
    controller.month = '01';
    controller.day = '01';
    scope.$digest();

    expect(scope.dateVal.substr(0, 10)).toBe('2001-01-01');
  });

  it('invalid date change', function() {
    var controller = dateFieldController();
    scope.vm = controller;
    controller.year = '2000';
    controller.month = '01';
    controller.day = '01';
    scope.$digest();

    controller.year = '2001';
    controller.month = '99';
    controller.day = '01';
    scope.$digest();
    expect(scope.dateVal).toBeUndefined();

    controller.year = '2001';
    controller.month = '02';
    controller.day = '01';
    scope.$digest();
    expect(scope.dateVal.substr(0, 10)).toBe('2001-02-01');
  });

  it('invoke controller', function() {
    var controller = dateFieldController();
    expect(controller).toBeDefined();
  });
});
