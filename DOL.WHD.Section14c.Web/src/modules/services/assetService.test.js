describe('stateService', function() {
    beforeEach(module('14c'));

    var stateService;

    beforeEach(inject(function($injector) {
        assetService = $injector.get('assetService');
    }));

    it('should return image url', function() {
        var imageName = 'whd_logo.jpg';
        var imagePath = assetService.loadImage(imageName);
    });


});
