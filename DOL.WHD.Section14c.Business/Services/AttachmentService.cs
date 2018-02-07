using System;
using System.Data;
using System.IO;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels;
using System.Collections.Generic;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using DOL.WHD.Section14c.Business.Helper;
using System.Security.Cryptography;
using System.Text;

namespace DOL.WHD.Section14c.Business.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private bool Disposed = false;

        public AttachmentService(IFileRepository fileRepository, IAttachmentRepository attachmentRepository)
        {
            _fileRepository = fileRepository;
            _attachmentRepository = attachmentRepository;
        }

        public Attachment UploadAttachment(string applicationId, byte[] bytes, string fileName, string fileType)
        {
            string FileEncryptKey = RandomString(40);
            var fileUpload = new Attachment()
            {
                FileSize = bytes.Length,
                MimeType = fileType,
                OriginalFileName = fileName,
                Deleted = false,
                ApplicationId = applicationId,
                EncryptionKey = FileEncryptKey
            };

            fileUpload.RepositoryFilePath = $@"{applicationId}\{fileUpload.Id}";

            // Encrypt file         
            byte[] keyInBytes = Encoding.UTF8.GetBytes(FileEncryptKey + fileName);
            // Hash the password with SHA256
            keyInBytes = SHA256.Create().ComputeHash(keyInBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytes, keyInBytes);
            _fileRepository.Upload(bytesEncrypted, fileUpload.RepositoryFilePath);

            _attachmentRepository.Add(fileUpload);
            _attachmentRepository.SaveChanges();

            return fileUpload;
        }

        public AttachementDownload DownloadAttachment(MemoryStream memoryStream, string EIN, Guid fileId)
        {
            var attachment = _attachmentRepository.Get()
                .Where(x => x.ApplicationId == EIN)
                .SingleOrDefault(x => x.Deleted == false && x.Id == fileId.ToString());

            if (attachment == null)
                throw new ObjectNotFoundException();

            var stream = _fileRepository.Download(memoryStream, attachment.RepositoryFilePath);

            // Decrypt file 
            byte[] bytesToBeDecrypted = stream.ToArray();
            byte[] keyInBytes = Encoding.UTF8.GetBytes(attachment.EncryptionKey + attachment.OriginalFileName);
            keyInBytes = SHA256.Create().ComputeHash(keyInBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, keyInBytes);
            stream = new MemoryStream(bytesDecrypted);

            return new AttachementDownload()
            {
                MemoryStream = stream,
                Attachment = attachment
            };
        }

        /// <summary>
        ///     Builds a list of PDF content data objects from the list
        ///     of attachments and a given HTML form
        /// </summary>
        /// <param name="attachments">
        ///     The list of attachments that should be in the document
        /// </param>
        /// <param name="applicationFormData">
        ///     The HTML form that should be in the document
        /// </param>
        /// <returns>
        ///     A list of PDF content data objects that can be sent to
        ///     the PDF API to generate a new PDF document
        /// </returns>
        public List<PDFContentData> PrepareApplicationContentsForPdfConcatenation(Dictionary<string, Attachment> attachments, List<string> applicationFormData)
        {
            var applicationData = new List<PDFContentData>();

            if (applicationFormData != null)
            {
                applicationData.Add(new PDFContentData() { HtmlString = applicationFormData, Type = "html" });
            }
            foreach (var attachment in attachments)
            {
                if (attachment.Value != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // Create file name for each attachment. 
                        // File name format: attachment type - original file name 
                        var fileName = string.Format("{0} - {1}", attachment.Key,  attachment.Value.OriginalFileName);
                        var stream = _fileRepository.Download(memoryStream, attachment.Value.RepositoryFilePath);
                        // Decrypt file 
                        byte[] bytesToBeDecrypted = stream.ToArray();
                        byte[] keyInBytes = Encoding.UTF8.GetBytes(attachment.Value.EncryptionKey + attachment.Value.OriginalFileName);
                        keyInBytes = SHA256.Create().ComputeHash(keyInBytes);
                        byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, keyInBytes);
                        stream = new MemoryStream(bytesDecrypted);
                        applicationData.Add(new PDFContentData() { Buffer = stream.ToArray(), Type = attachment.Value.MimeType, FileName = fileName });
                    }
                }
            }
            return applicationData;
        }

        public void DeleteApplicationAttachements(string applicationId)
        {
            var attachments = _attachmentRepository.Get()
                .Where(x => x.ApplicationId == applicationId && x.Deleted == false);
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    attachment.Deleted = true;
                }
                _attachmentRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Get all attachments from an application after the application has been submitted.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Dictionary<string, Attachment> GetApplicationAttachments(ref ApplicationSubmission application)
        {
            var attachments = new Dictionary<string, Attachment>();
            var applicationSubmission = application;
            if (application != null)
            {
                var count = 1;
                if (application.Employer?.SCAAttachments != null)
                {
                    foreach (var item in application.Employer.SCAAttachments)
                    {
                        var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == item.SCAAttachmentId);
                        attachments.Add(string.Format("SCA Wage Determination Attachment {0}", (count++) ), attachment);
                    }
                }

                if (application.PieceRateWageInfo?.SCAAttachments != null)
                {
                    count = 1;
                    foreach (var item in application.PieceRateWageInfo.SCAAttachments)
                    {
                        var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == item.SCAAttachmentId);
                        attachments.Add(string.Format("Piece Rate Wage Info ScaWage Determination Attachment  {0}", (count++)), attachment);
                    }
                }

                if (application.PieceRateWageInfo?.AttachmentId != null)
                {
                    var attachmentId = application.PieceRateWageInfo.AttachmentId;
                    var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == attachmentId);
                    attachments.Add("Piece Rate Wage Info Attachment", attachment);
                }

                if (application.HourlyWageInfo?.SCAAttachments != null)
                {
                    count = 1;
                    foreach (var item in application.HourlyWageInfo.SCAAttachments)
                    {
                        var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == item.SCAAttachmentId);
                        attachments.Add(string.Format("Hourly Wage Info ScaWage Determination Attachment  {0}", (count++)), attachment);
                    }
                }

                if (application.HourlyWageInfo?.MostRecentPrevailingWageSurvey?.AttachmentId != null)
                {
                    var attachmentId = application.HourlyWageInfo.MostRecentPrevailingWageSurvey.AttachmentId;
                    var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == attachmentId);
                    attachments.Add("Hourly Wage Info SCA Wage Determination Attachment", attachment);
                }

                if (application.HourlyWageInfo?.AttachmentId != null)
                {
                    var attachmentId = application.HourlyWageInfo.AttachmentId;
                    var attachment = _attachmentRepository.Get().SingleOrDefault(x => x.Id == attachmentId);
                    attachments.Add("Hourly Wage Info Attachmen", attachment);
                }
            }
            return attachments;
        }

        /// <summary>
        /// Delete Attachment
        /// </summary>
        /// <param name="applicationId">Application Id</param>
        /// <param name="fileId">File Id</param>
        public void DeleteAttachement(string applicationId, Guid fileId)
        {
            var attachment = _attachmentRepository.Get()
                .Where(x => x.ApplicationId == applicationId)
                .SingleOrDefault(x => x.Deleted == false && x.Id == fileId.ToString());

            if (attachment == null)
                throw new ObjectNotFoundException();

            attachment.Deleted = true;

            _attachmentRepository.SaveChanges();
        }

        /// <summary>
        ///     Given a 14c application and an HTML template string,
        ///     build a populated HTML string
        /// </summary>
        /// <param name="application">
        ///     The 14c application object
        /// </param>
        /// <param name="templateString">
        ///     The HTML template string to populate
        /// </param>
        /// <returns>
        ///     A popualted HTML string
        /// </returns>
        public string GetApplicationFormViewContent(ApplicationSubmission application, string templateString)
        {
            string tempString = string.Empty;
            tempString = ApplicationFormViewHelper.PopulateHtmlTemplateWithApplicationData(application, templateString);
            return tempString;
        }

        /// <summary>
        /// AES encryption
        /// </summary>
        /// <param name="bytesToBeEncrypted">Byte array </param>
        /// <param name="encryptKey">encryption key</param>
        /// <returns>Byte Array</returns>
        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] encryptKey)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(encryptKey, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        /// <summary>
        /// AES decryption
        /// </summary>
        /// <param name="bytesToBeDecrypted">Byte Array</param>
        /// <param name="encryptKey">Encryption key</param>
        /// <returns>Byte Array</returns>
        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] encryptKey)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(encryptKey, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        private string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            var chars = Enumerable.Range(0, length).Select(x => pool[rand.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _attachmentRepository.Dispose();
            }
        }
    }
}
