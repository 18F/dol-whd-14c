describe('dateFilter', function() {
  var dateFilter;

  beforeEach(module('14c'));

  beforeEach(
    inject(function(_dateFilterFilter_) {
      dateFilter = _dateFilterFilter_;
    })
  );

  it('should convert date to mm/dd/yyyy format', function() {
    var date = '2016-01-01';
    var formatted = dateFilter(date);
    expect(formatted).toEqual('1/1/2016');
  });

  it('should handle invalid date', function() {
    var date = 'bad date';
    var formatted = dateFilter(date);
    expect(formatted).toEqual('INVALID DATE');
  });
});
