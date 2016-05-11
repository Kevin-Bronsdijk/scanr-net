using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using ScanR.Model;

namespace ScanR.Http
{
    internal static class ApiHelper
    {
        public static void ThrowIfNullOrEmpty(this string value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
            if (value == string.Empty)
            {
                throw new ArgumentException("argument cannot be null or empty.", name);
            }
        }

        public static void ImageFileValidation(Uri url)
        {
            ImageFileValidation(Path.GetFileName(url.AbsolutePath));
        }

        public static void ImageFileValidation(string filename)
        {
            filename.ThrowIfNullOrEmpty("filename");

            FileValidation(filename);
        }

        public static void PdfFileValidation(Uri url)
        {
            PdfFileValidation(Path.GetFileName(url.AbsolutePath));
        }

        public static void PdfFileValidation(string filename)
        {
            filename.ThrowIfNullOrEmpty("filename");

            FileValidation(filename, false);
        }

        private static void FileValidation(string filename, bool isImage = true)
        {
            var supportedExtensions = isImage ? new List<string> { ".bmp", ".pnm", ".png", ".jpg", ".jpeg", ".tiff", ".gif", ".ps", ".webp" } : 
                new List<string> { ".pdf" };

            if (Path.HasExtension(filename))
            {
                var extension = Path.GetExtension(filename);

                if (!supportedExtensions.Any(e => extension != null && e.Contains(extension.ToLower())))
                {
                    throw new ApplicationException("The file must have an supported extension");
                }
            }
            else
            {
                throw new ApplicationException("The file must have an extension");
            }
        }

        public static string GetLanguageAbbreviation(Language language)
        {
            var languages = new Dictionary<string, string>
            {
                {"Arabic", "ara"},
                {"Azerbauijani", "aze"},
                {"Bulgarian", "bul"},
                {"Catalan", "cat"},
                {"Czech", "ces"},
                {"SimplifiedChinese", "chi_sim"},
                {"TraditionalChinese", "chi_tra"},
                {"Cherokee", "chr"},
                {"Danish", "dan"},
                {"DanishFraktur", "dan-frak"},
                {"German", "deu"},
                {"Greek", "ell"},
                {"English", "eng"},
                {"Old English", "enm"},
                {"Esperanto", "epo"},
                {"Estonian", "est"},
                {"Finnish", "fin"},
                {"French", "fra"},
                {"OldFrench", "frm"},
                {"Galician", "glg"},
                {"Hebrew", "heb"},
                {"Hindi", "hin"},
                {"Croation", "hrv"},
                {"Hungarian", "hun"},
                {"Indonesian", "ind"},
                {"Italian", "ita"},
                {"Japanese", "jpn"},
                {"Korean", "kor"},
                {"Latvian", "lav"},
                {"Lithuanian", "lit"},
                {"Dutch", "nld"},
                {"Norwegian", "nor"},
                {"Polish", "pol"},
                {"Portuguese", "por"},
                {"Romanian", "ron"},
                {"Russian", "rus"},
                {"Slovakian", "slk"},
                {"Slovenian", "slv"},
                {"Albanian", "sqi"},
                {"Spanish", "spa"},
                {"Serbian", "srp"},
                {"Swedish", "swe"},
                {"Tamil", "tam"},
                {"Telugu", "tel"},
                {"Tagalog", "tgl"},
                {"Thai", "tha"},
                {"Turkish", "tur"},
                {"Ukrainian", "ukr"},
                {"Vietnamese", "vie"}
            };

            string abbreviation;
            languages.TryGetValue(language.ToString(), out abbreviation);

            return abbreviation;
        }
    }
}