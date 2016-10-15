using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels;

namespace DOL.WHD.Section14c.Business.Services
{
    public class SaveService : ISaveService
    {
        private readonly ISaveRepository _repository;
        private readonly IFileRepository _fileRepository;

        public SaveService(ISaveRepository repository, IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }

        public ApplicationSave GetSave(string EIN)
        {
            return _repository.Get().SingleOrDefault(x => x.EIN == EIN);
        }

        public void AddOrUpdate(string EIN, string state)
        {
            var applicationSave = GetSave(EIN);
            if (applicationSave != null)
            {
                // if save already exists just update the state
                applicationSave.ApplicationState = state;

                _repository.SaveChanges();
            }
            else
            {
                applicationSave = new ApplicationSave
                {
                    EIN = EIN,
                    ApplicationState = state
                };

                _repository.Add(applicationSave);
            }
        }

        public Attachment UploadAttachment(string EIN, MemoryStream memoryStream, string fileName, string fileType)
        {
            var fileUpload = new Domain.Models.Submission.Attachment()
            {
                FileSize = memoryStream.Length,
                MimeType = fileType,
                OriginalFileName = fileName,
                Deleted = false
            };

            fileUpload.RepositoryFilePath = $@"{EIN}\{fileUpload.Id}";

            _fileRepository.Upload(memoryStream, fileUpload.RepositoryFilePath);

            var applicationSave = _repository.Get().SingleOrDefault(x => x.EIN == EIN);
            if (applicationSave == null)
            {
                applicationSave = new ApplicationSave() { EIN = EIN, ApplicationState = "{}" };
                _repository.Add(applicationSave);
            }

            applicationSave.Attachments.Add(fileUpload);
            _repository.SaveChanges();

            return fileUpload;
        }

        public AttachementDownload DownloadAttachment(MemoryStream memoryStream, string EIN, Guid fileId)
        {
            var attachment = _repository.Get()
                    .Where(x => x.EIN == EIN && x.Attachments.Where(y => !y.Deleted).Select(a => a.Id).Contains(fileId)) // trim by EIN, FileId and not deleted
                    .Select(x => x.Attachments.FirstOrDefault()).SingleOrDefault();

            if (attachment == null)
                throw new ObjectNotFoundException();

            var stream = _fileRepository.Download(memoryStream, attachment.RepositoryFilePath);

            if (stream == null)
                throw new FileNotFoundException();

            return new AttachementDownload()
            {
                MemoryStream = stream,
                Attachment = attachment
            };
        }

        public void DeleteAttachement(string EIN, Guid fileId)
        {
            var attachment = _repository.Get()
                    .Where(x => x.EIN == EIN && x.Attachments.Where(y => !y.Deleted).Select(a => a.Id).Contains(fileId)) // trim by EIN, FileId and not deleted
                    .Select(x => x.Attachments.FirstOrDefault()).SingleOrDefault();

            if (attachment == null)
                throw new ObjectNotFoundException();

            attachment.Deleted = true;

            _repository.SaveChanges();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
