describe('attachmentFieldController', function() {

    beforeEach(module('14c'));

    beforeEach(inject(function ($rootScope, $controller, _$q_, apiService) {
        scope = $rootScope.$new();
        $q = _$q_;
        mockApiService = apiService;

        attachmentFieldController = function() {
            return $controller('attachmentFieldController', {
                '$scope': scope,
                'apiService': mockApiService
            });
        };

        uploadAttachment = $q.defer();
        spyOn(mockApiService, 'uploadAttachment').and.returnValue(uploadAttachment.promise);

        deleteAttachment = $q.defer();
        spyOn(mockApiService, 'deleteAttachment').and.returnValue(deleteAttachment.promise);


    }));

    it('invoke controller', function() {
        var controller = attachmentFieldController();
    });

    it('attachment selected no files', function() {
        var controller = attachmentFieldController();
        fileInput = { files: [] };
        controller.onAttachmentSelected(fileInput);
    });


    it('attachment selected files failed upload', function() {
        var controller = attachmentFieldController();
        fileInput = { files: [ {} ], value: '' };
        controller.onAttachmentSelected(fileInput);
        uploadAttachment.reject({});
        scope.$apply();
        expect(fileInput.value).toBe(''); 
    });    

    it('attachment selected files uploaded successful', function() {
        var controller = attachmentFieldController();
        fileInput = { files: [ {}], value: '' };
        controller.onAttachmentSelected(fileInput);
        uploadAttachment.resolve({ data: [ {id: 1, originalFileName: 'name'}]});
        scope.$apply();
        expect(scope.attachmentId).toBe(1); 
        expect(scope.attachmentName).toBe('name'); 
        expect(fileInput.value).toBe(''); 
    });    


    it('delete attachment successful, clears attachment fields', function() {
        var controller = attachmentFieldController();
        scope.attachmentId = 1;
        scope.attachmentName = 'name';
        controller.deleteAttachment(1);
        deleteAttachment.resolve({});
        scope.$apply();
        expect(scope.attachmentId).toBe(undefined);
        expect(scope.attachmentName).toBe(undefined);
    }); 

    it('delete attachment failer, clears attachment fields', function() {
        var controller = attachmentFieldController();
        scope.attachmentId = 1;
        scope.attachmentName = 'name';
        controller.deleteAttachment(1);
        deleteAttachment.reject({});
        scope.$apply();
        expect(scope.attachmentId).toBe(undefined);
        expect(scope.attachmentName).toBe(undefined);
    });                   

});