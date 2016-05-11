using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ScanR.Http;
using ScanR.Model;

namespace ScanR
{
    public class ScanRClient : IDisposable
    {
        private ScanRConnection _connection;

        public ScanRClient(ScanRConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            _connection = connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<IApiResponse<OcrResult>> Scan(Uri fileUrl, Language language)
        {
            return Scan(fileUrl, language, default(CancellationToken));
        }

        public Task<IApiResponse<OcrResult>> Scan(Uri fileUrl, Language language, CancellationToken cancellationToken)
        {
            ApiHelper.ImageFileValidation(fileUrl);

            var apiRequest = new ScanRApiRequest(new OcrRequest(), "ocr");
            apiRequest.AddQueryParameter("lang", ApiHelper.GetLanguageAbbreviation(language));
            apiRequest.AddQueryParameter("url", fileUrl.AbsoluteUri);

            var message = _connection.Execute<OcrResult>(apiRequest, cancellationToken);

            return message;
        }

        public Task<IApiResponse<OcrResult>> Scan(string filePath, Language language)
        {
            return Scan(filePath, language, default(CancellationToken));
        }

        public Task<IApiResponse<OcrResult>> Scan(string filePath, Language language, CancellationToken cancellationToken)
        {
            ApiHelper.ImageFileValidation(filePath);
            if (!File.Exists(filePath)) { throw new FileNotFoundException(); }

            var file = File.ReadAllBytes(filePath);
            
            var message = Scan(file, Path.GetFileName(filePath), language, cancellationToken);

            return message;
        }

        public Task<IApiResponse<OcrResult>> Scan(byte[] file, string filename, Language language)
        {
            return Scan(file, filename, language, default(CancellationToken));
        }

        public Task<IApiResponse<OcrResult>> Scan(byte[] file, string filename, Language language, CancellationToken cancellationToken)
        {
            ApiHelper.ImageFileValidation(filename);
            if (file == null) {  throw new ArgumentNullException(nameof(file)); }

            var apiRequest = new ScanRApiRequest(new OcrRequest(), "ocr");
            apiRequest.AddQueryParameter("lang", ApiHelper.GetLanguageAbbreviation(language));

            var message = _connection.ExecuteUpload<OcrResult>(apiRequest, file, filename, cancellationToken);

            return message;
        }

        public Task<IApiResponse<OcrMultiPageResult>> ScanPdf(Uri fileUrl, Language language)
        {
            return ScanPdf(fileUrl, language, default(CancellationToken));
        }

        public Task<IApiResponse<OcrMultiPageResult>> ScanPdf(Uri fileUrl, Language language, CancellationToken cancellationToken)
        {
            ApiHelper.PdfFileValidation(fileUrl);

            var apiRequest = new ScanRApiRequest(new OcrRequest(), "ocr");
            apiRequest.AddQueryParameter("lang", ApiHelper.GetLanguageAbbreviation(language));
            apiRequest.AddQueryParameter("url", fileUrl.AbsoluteUri);

            var message = _connection.Execute<OcrMultiPageResult>(apiRequest, cancellationToken);

            return message;
        }

        public Task<IApiResponse<OcrMultiPageResult>> ScanPdf(string filePath, Language language)
        {
            return ScanPdf(filePath, language, default(CancellationToken));
        }

        public Task<IApiResponse<OcrMultiPageResult>> ScanPdf(string filePath, Language language, CancellationToken cancellationToken)
        {
            ApiHelper.PdfFileValidation(filePath);
            if (!File.Exists(filePath)) { throw new FileNotFoundException(); }

            var file = File.ReadAllBytes(filePath);

            var message = ScanPdf(file, Path.GetFileName(filePath), language, cancellationToken);

            return message;
        }

        public Task<IApiResponse<OcrMultiPageResult>> ScanPdf(byte[] file, string filename, Language language)
        {
            return ScanPdf(file, filename, language, default(CancellationToken));
        }

        public Task<IApiResponse<OcrMultiPageResult>> ScanPdf(byte[] file, string filename, Language language, CancellationToken cancellationToken)
        {
            ApiHelper.PdfFileValidation(filename);
            if (file == null) { throw new ArgumentNullException(nameof(file)); }

            var apiRequest = new ScanRApiRequest(new OcrRequest(), "ocr");
            apiRequest.AddQueryParameter("lang", ApiHelper.GetLanguageAbbreviation(language));

            var message = _connection.ExecuteUpload<OcrMultiPageResult>(apiRequest, file, filename, cancellationToken);

            return message;
        }

        ~ScanRClient()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}