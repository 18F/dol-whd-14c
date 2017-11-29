describe('resultsTable', function() {
  beforeEach(module('14c'));
  var element = angular.element('<results-table results="results" columns="columns"></results-table>');
  beforeEach(function() {
    inject(function($rootScope, $compile) {
      var scope = $rootScope.$new();
      scope.columns = [];
      scope.results = [ ];

      $compile(element)(scope);
      scope.$digest();
    });


  });

  it('invoke directive', function() {
    expect(element).toBeDefined();
  });
});
