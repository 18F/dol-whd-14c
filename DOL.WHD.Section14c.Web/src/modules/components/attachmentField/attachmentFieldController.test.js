describe('attachmentFieldController', function() {
  var scope, $q, mockApiService, attachmentFieldController;
  var uploadAttachment, deleteAttachment;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService) {
      scope = $rootScope.$new();
      $q = _$q_;
      mockApiService = apiService;

      attachmentFieldController = function() {
        return $controller('attachmentFieldController', {
          $scope: scope,
          apiService: mockApiService
        });
      };

      uploadAttachment = $q.defer();
      spyOn(mockApiService, 'uploadAttachment').and.returnValue(
        uploadAttachment.promise
      );

      deleteAttachment = $q.defer();
      spyOn(mockApiService, 'deleteAttachment').and.returnValue(
        deleteAttachment.promise
      );
    })
  );

  it('attachment selected no files, file value should stay the same', function() {
    var controller = attachmentFieldController();
    var fileInput = { files: [], value: 1 };
    controller.onAttachmentSelected(fileInput);
    expect(controller.upload.status).toBe('Uploading');
    expect(fileInput.value).toBe(1);
  });

  it('attachment selected files failed upload', function() {
    var controller = attachmentFieldController();
    var fileInput = { files: [{name: 'name1.pdf'}] };
    controller.onAttachmentSelected(fileInput);
    uploadAttachment.reject({});
    scope.$apply();

    //TODO assert that error is displayed when code is added
    expect(fileInput.value).toBe('');
    expect(controller.upload.status).toBe('Invalid')
  });

  it('attachment selected files uploaded successful', function() {
    var controller = attachmentFieldController();
    var fileInput = { files: [{name: 'name1.pdf'}] };
    controller.onAttachmentSelected(fileInput);
    uploadAttachment.resolve({ data: [{ id: 1, originalFileName: 'name' }] });
    scope.$apply();
    expect(scope.attachmentId).toBe(1);
    expect(scope.attachmentName).toBe('name');
    expect(fileInput.value).toBe('');
    expect(controller.upload.status).toBe('Success');
  });

  it('delete attachment successful, clears attachment fields', function() {
    var controller = attachmentFieldController();
    scope.attachmentId = 1;
    scope.attachmentName = 'name';
    controller.deleteAttachment(1);
    deleteAttachment.resolve({});
    scope.$apply();
    expect(scope.attachmentId).toBe(undefined);
    expect(scope.attachmentName).toBe(undefined)
    expect(controller.upload.status).toBe('NoFile');
  });

  it('delete attachment failer, clears attachment fields', function() {
    var controller = attachmentFieldController();
    scope.attachmentId = 1;
    scope.attachmentName = 'name';
    controller.deleteAttachment(1);
    deleteAttachment.reject({});
    scope.$apply();

    //TODO assert that error is displayed when code is added
    expect(scope.attachmentId).toBe(undefined);
    expect(scope.attachmentName).toBe(undefined);
    expect(controller.upload.status).toBe('Failure');
  });
});
