describe('stateService', function() {
  beforeEach(module('14c'));

  var assetService;

  beforeEach(
    inject(function($injector) {
      assetService = $injector.get('assetService');
    })
  );

  it('should return image url', function() {
    var imageName = 'whd_logo.jpg';
    var imagePath = assetService.loadImage(imageName);
    expect(imagePath).toBeTruthy();
    expect(imagePath.endsWith(imageName)).toBe(true);
  });
});
