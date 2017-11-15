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

    expect(momentMock(scope.dateVal).format('YYYY-MM-DD')).toBe('2001-01-01');
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

    controller.year = '2001';
    controller.month = '02';
    controller.day = '01';
    scope.$digest();
  });

  it('invoke controller', function() {
    momentMock = function() {
      return {
        isSame: function() {
          return false;
        },
        isValid: function() {
          return true;
        },
        isDate: function() {
          return true;
        },
        month: function() {
          return true;
        },
        date: function() {
          return true;
        },
        year: function() {
          return true;
        }
      };
    };
    momentMock.isDate = function() {
      return true;
    };
    var controller = dateFieldController();
    scope.vm = controller;
    scope.$digest();
  });
});
