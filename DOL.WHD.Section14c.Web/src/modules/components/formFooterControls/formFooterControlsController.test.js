describe('formFooterControlsController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, _$q_, navService, apiService) {
        scope = $rootScope.$new();
        $q = _$q_;

        mockNavService = navService;
        mockApiService = apiService;

        spyOn(mockNavService,'hasNext');
        spyOn(mockNavService,'hasBack');
        spyOn(mockNavService,'getNextSection');

        saveApplication = $q.defer();
        spyOn(mockApiService, 'saveApplication').and.returnValue(saveApplication.promise);

        formFooterControlsController = function() {
            return $controller('formFooterControlsController', {
                '$scope': scope, 
                'navService': mockNavService
            });
        };
    }));

    it('invoke controller', function() {
        var controller = formFooterControlsController();
    });
    it('next label', function() {
        var controller = formFooterControlsController();
        scope.vm = controller;
        mockNavService.nextLabel = 'next';
        scope.$digest();
    });  

    it('next label undefined', function() {
        var controller = formFooterControlsController();
        scope.vm = controller;
        mockNavService.nextLabel = undefined;
        scope.$digest();
    });       

    it('save', function() {
        var controller = formFooterControlsController();
        controller.doSave();
        scope.$apply();
    });   

    it('onNextClick no next', function() {
        var controller = formFooterControlsController();
        controller.onNextClick();
        scope.$apply();
    });      

    it('onNextClick', function() {
        var controller = formFooterControlsController();
        controller.hasNext = '';
        controller.onNextClick();
        scope.$apply();
    });    

    it('onBackClick no back', function() {
        var controller = formFooterControlsController();
        controller.onBackClick();
        scope.$apply();
    });    

    it('onBackClick', function() {
        var controller = formFooterControlsController();
        controller.hasBack = '';
        controller.onBackClick();
        scope.$apply();
    });            

    it('onSaveClick', function() {
        var controller = formFooterControlsController();
        controller.onSaveClick();
        scope.$apply();
    });           
    
});