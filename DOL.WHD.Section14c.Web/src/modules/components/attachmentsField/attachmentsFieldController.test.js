describe('attachmentsFieldController', function() {
  var scope, $q, mockApiService, attachmentsFieldController;
  var uploadAttachment, deleteAttachment, mockStateService, mockEnv;
  var controller;

  beforeEach(module('14c'));

  beforeEach(
    inject(function($rootScope, $controller, _$q_, apiService, _env, stateService) {
      scope = $rootScope.$new();
      mockEnv = _env;
      mockStateService = stateService;
      scope.modelPrefix = "modelPrefix";
      scope.inputId = "inputId";

      $q = _$q_;
      mockApiService = apiService;

      mockEnv.allowedFileTypes = '["pdf", "jpg", "JPG", "jpeg", "JPEG", "png", "PNG", "csv", "CSV", "PDF"]';
      mockStateService.formData = {
        modelPrefix: {
          inputId: [{scaAttachmentId: '12345'}]
        }
      };

      attachmentsFieldController = function() {

        return $controller('attachmentsFieldController', {
          $scope: scope,
          _env: mockEnv,
          apiService: mockApiService,
          stateService: mockStateService
        });
      };

      controller = attachmentsFieldController({
        scope: scope,
        _env: mockEnv,
        stateService: mockStateService,
        apiService: mockApiService
      });

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

  ['pdf', 'jpg', 'JPG', 'jpeg', 'JPEG', 'png', 'PNG', 'csv', 'CSV', 'PDF'].forEach(function(extension) {
    it('should allow file of type ' + extension, function() {
      scope.modelPrefix = "modelPrefix";
      scope.inputId = "inputId";
      mockEnv.allowedFileTypes = '["pdf", "jpg", "JPG", "jpeg", "JPEG", "png", "PNG", "csv", "CSV", "PDF"]';
      mockStateService.formData = {
        modelPrefix: {
          inputId: [{scaAttachmentId: '12345'}]
        }
      };
      var fileInput = { files: [{name: 'file.' + extension, size: 1000}] };
      controller = attachmentsFieldController({
        scope: scope,
        _env: mockEnv,
        stateService: mockStateService,
        apiService: mockApiService
      });
      controller.onAttachmentSelected(fileInput);

      uploadAttachment.resolve({ data: [{ id: 1, originalFileName: 'name1.pdf' }] });

      scope.$apply();
      expect(scope.attachmentName).toBe('name1.pdf');
      expect(fileInput.value).toBe('');
      expect(scope.upload.status).toBe('Success');
      expect(scope.upload.message).toBe('File was uploaded successfully.');
      expect(mockApiService.uploadAttachment).toHaveBeenCalled();
    });
  });

  ['docx', 'gif', 'avi', 'iso'].forEach(function(extension) {
    it('should not allow file of type ' + extension, function() {
      mockEnv.allowedFileTypes = '["pdf", "jpg", "JPG", "jpeg", "JPEG", "png", "PNG", "csv", "CSV", "PDF"]';
      mockStateService.formData = {
        modelPrefix: {
          inputId: [{scaAttachmentId: '12345'}]
        }
      };
      scope.modelPrefix = "modelPrefix";
      scope.inputId = "inputId";

      var fileInput = { files: [{name: 'file.' + extension, size: 1000}] };
      controller = attachmentsFieldController({
        scope: scope,
        _env: mockEnv,
        stateService: mockStateService,
        apiService: mockApiService
      });

      controller.onAttachmentSelected(fileInput);

      uploadAttachment.reject({ data: [{ id: 1, originalFileName: 'name1.pdf' }] });
      scope.$apply();
      expect(scope.attachmentName).toBe('file.' + extension);
      expect(fileInput.value).toBe('');
      expect(scope.upload.status).toBe('Invalid');
      expect(scope.upload.message).toBe('Invalid File Type.');
      expect(mockApiService.uploadAttachment).not.toHaveBeenCalled();
    });
  });

  it('prevents multiple files from being uploaded', function() {
    scope.allowMultiUpload = false;
    var fileInput = { files: [{name: 'name1.pdf', size: 1000}] };
    controller.onAttachmentSelected(fileInput);
    uploadAttachment.resolve({ data: [{ id: 1, originalFileName: 'name1.pdf' }] });
    scope.$apply();
    expect(scope.restrictUpload).toBe(true);
    expect(scope.upload.status).toBe('Success');
  });


  it('it allows multiple files to be uploaded', function() {
    scope.allowMultiUpload = true;
    var fileInput = { files: [{name: 'name1.pdf', size: 1000}] };
    controller.onAttachmentSelected(fileInput);
    uploadAttachment.resolve({ data: [{ id: 1, originalFileName: 'name1.pdf' }] });
    scope.$apply();
    expect(scope.restrictUpload).toBe(false);
    expect(scope.upload.status).toBe('Success');
  });

  it('it should prevent uploads of larger than 5MB', function() {
    var fileInput = { files: [{name: 'name1.pdf', size: '10000000000'}] };
    controller.onAttachmentSelected(fileInput);
    expect(scope.upload.status).toBe('Invalid');
    expect(scope.upload.message).toBe('File Size too large.');
    expect(mockApiService.uploadAttachment).not.toHaveBeenCalled();
  });

  it('attachment selected no files, file value should stay the same', function() {
    var fileInput = { files: [{name: 'name1.pdf', size: 1000}], value: 1 };
    controller.onAttachmentSelected();
    expect(scope.upload.status).toBe('NoFile');
    expect(fileInput.value).toBe(1);
  });

  it('attachment selected files failed upload', function() {
    var fileInput = { files: [{name: 'name1.pdf'}] };
    controller.onAttachmentSelected(fileInput);
    uploadAttachment.reject({});
    scope.$apply();

    //TODO assert that error is displayed when code is added
    expect(fileInput.value).toBe('');
    expect(scope.upload.status).toBe('Server Error');
  });

  it('attachment selected files uploaded successful', function() {
    var fileInput = { files: [{name: 'name1.pdf', size: 1000}], value: 1 };
    controller.onAttachmentSelected(fileInput);
    uploadAttachment.resolve({ data: [{ id: 1, originalFileName: 'name1.pdf' }] });
    scope.$apply();
    expect(scope.attachmentId).toBe(1);
    expect(scope.attachmentName).toBe('name1.pdf');
    expect(fileInput.value).toBe('');
    expect(scope.upload.status).toBe('Success');
  });

  it('delete attachment successful, clears attachment fields', function() {
    scope.attachmentId = 1;
    scope.attachmentName = 'name';
    scope.attachments = [{scaAttachmentId: scope.attachmentId , attachmentName: scope.attachmentName}];
    controller.deleteAttachment(scope.attachmentId, scope.attachmentName);
    deleteAttachment.resolve({});
    scope.$apply();
    expect(scope.attachmentId).toBe(undefined);
    expect(scope.attachmentName).toBe(undefined);
    expect(scope.upload.status).toBe('NoFile');
    expect(scope.attachments.length).toEqual(0);
    expect(scope.restrictUpload).toEqual(false);
    expect(scope.modalIsVisible).toEqual(false);
  });

  it('delete attachment failer, does not reset attachment fields', function() {
    scope.modalIsVisible = true;
    scope.attachmentId = 1;
    scope.attachmentName = 'name';
    controller.deleteAttachment(scope.attachmentId, scope.attachmentName);
    deleteAttachment.reject({});
    scope.$apply();

    //TODO assert that error is displayed when code is added
    expect(scope.attachmentId).toBe(1);
    expect(scope.attachmentName).toBe('name');
    expect(scope.upload.status).toBe('Failure');
    expect(scope.modalIsVisible).toEqual(true);
  });
});
