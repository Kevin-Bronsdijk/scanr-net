using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScanR;
using ScanR.Http;
using ScanR.Model;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ConnectionCreate_EmptyKeyError_IsTrue()
        {
            try
            {
                ScanRConnection.Create("");
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.IsTrue(true, "Exception");
            }
        }

        [TestMethod]
        public void ConnectionCreate_NullKeyError_IsTrue()
        {
            try
            {
                ScanRConnection.Create(null);
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.IsTrue(true, "Exception");
            }
        }

        [TestMethod]
        public void Client_NullConnectionError_IsTrue()
        {
            try
            {
                new ScanRClient(null);
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.IsTrue(true, "Exception");
            }
        }

        [TestMethod]
        public void Client_NoErrors_IsTrue()
        {
            try
            {
                var connection = ScanRConnection.Create("key");
                var client = new ScanRClient(connection);

                Assert.IsTrue(client != null);
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "Exception");
            }
        }

        [TestMethod]
        public void ConnectionCreate_Dispose_IsTrue()
        {
            var connection = ScanRConnection.Create("key");

            try
            {
                connection.Dispose();

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "Exception");
            }
        }

        [TestMethod]
        public void Client_MustProvideAConnection_IsTrue()
        {
            try
            {
                var Client = new ScanRClient(null);

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_Dispose_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Dispose();

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "Exception");
            }
        }

        [TestMethod]
        public void Client_RequestUploadNoFileNameError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    new  byte[] { 1 }, 
                    string.Empty, 
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestUploadNoFileError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    null,
                    "test.jpg",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestUploadFileNotSupportedError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    new byte[] { 1 },
                    "test.dll",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestUploadFileNoExtensionError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    new byte[] { 1 },
                    "test",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestUriNoExtensionError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    new Uri("http://www.devslice.net/test"), 
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestUriFileNotSupportedError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    new Uri("http://www.devslice.net/test.pdp"),
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestFileNotSupported_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    "image1.txt",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestFileNoExtensionError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    "image1",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_RequestFileNotFoundError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.Scan(
                    "test.jpg",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void Client_PdfRequestUploadNoFileNameError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    new byte[] { 1 },
                    string.Empty,
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestUploadNoFileError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    null,
                    "test.pdf",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestUploadFileNotSupportedError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    new byte[] { 1 },
                    "test.dll",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestUploadFileNoExtensionError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    new byte[] { 1 },
                    "test",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestUriNoExtensionError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    new Uri("http://www.devslice.net/test"),
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestUriFileNotSupportedError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    new Uri("http://www.devslice.net/test.pdp"),
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestFileNotSupported_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    "image1.txt",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestFileNoExtensionError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    "image1",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Client_PdfRequestFileNotFoundError_IsTrue()
        {
            var connection = ScanRConnection.Create("key");
            var client = new ScanRClient(connection);

            try
            {
                client.ScanPdf(
                    "test.pdf",
                    Language.SimplifiedChinese
                    );

                Assert.IsTrue(false, "No exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}