using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScanR;
using ScanR.Http;
using ScanR.Model;

namespace Tests
{
    [TestClass]
    [DeploymentItem("Images")]
    [Ignore] // Only test build locally
    public class IntergrationTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void Client_RequestUriFromTheWebinvalidToken_IsTrue()
        {
            var testData = new Tuple<Uri, string>(new Uri("http://cdn.devslice.net/blog/wp-content/uploads/2015/01/Azure-Kraken-io.png"), "Kraken\n\n");
            var connection = ScanRConnection.Create("invalid-token");
            var client = new ScanRClient(connection);

            var response = client.Scan(testData.Item1, Language.English);

            var result = response.Result;

            Assert.IsTrue(result.Success == false);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.Unauthorized);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Error));
            Assert.IsTrue(result.Body == null);
        }

        [TestMethod]
        public void Client_RequestUriFromTheWeb_IsTrue()
        {
            var testData = new Tuple<Uri, string>(new Uri("http://cdn.devslice.net/blog/wp-content/uploads/2015/01/Azure-Kraken-io.png"), "Kraken\n\n");
            var client = HelperFunctions.CreateWorkingClient();

            var response = client.Scan(testData.Item1, Language.English);

            var result = response.Result;

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            Assert.IsTrue(result.Body != null);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Body.Text));
            Assert.IsTrue(result.Body.Text == testData.Item2);
        }

        [TestMethod]
        public void Client_RequestUploadDevSliceNet_IsTrue()
        {
            const string testImageName = TestData.LocalTestImage;
            var client = HelperFunctions.CreateWorkingClient();
            var image = File.ReadAllBytes(TestData.LocalTestImage);

            var response = client.Scan(
                image, 
                testImageName,
                Language.English);

            var result = response.Result;

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            Assert.IsTrue(result.Body != null);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Body.Text));
            Assert.IsTrue(result.Body.Text == "DevSliceNet\n\n");
        }

        [TestMethod]
        public void Client_RequestUploadDevSliceNetFilePath_IsTrue()
        {
            var client = HelperFunctions.CreateWorkingClient();

            var response = client.Scan(
                TestData.LocalTestImage,
                Language.English);

            var result = response.Result;

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            Assert.IsTrue(result.Body != null);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Body.Text));
            Assert.IsTrue(result.Body.Text == "DevSliceNet\n\n");
        }

        [TestMethod]
        public void Client_PdfRequestUploadDevSliceNet_IsTrue()
        {
            var client = HelperFunctions.CreateWorkingClient();
            var image = File.ReadAllBytes(TestData.LocalTestPdf);

            var response = client.ScanPdf(
                image,
                TestData.LocalTestPdf,
                Language.English);

            var result = response.Result;

            // PDF support still very limited, sample PDF failed.

            //Assert.IsTrue(result.Success);
            //Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            //Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            //Assert.IsTrue(result.Body != null);
        }

        [TestMethod]
        public void Client_PdfRequestUploadDevSliceNetFilePath_IsTrue()
        {
            var client = HelperFunctions.CreateWorkingClient();

            var response = client.ScanPdf(
                TestData.LocalTestPdf,
                Language.English);

            var result = response.Result;

            // PDF support still very limited, sample PDF failed.

            //Assert.IsTrue(result.Success);
            //Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            //Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            //Assert.IsTrue(result.Body != null);
        }

        [TestMethod]
        public void Client_RequestUriPdfFromTheWeb_IsTrue()
        {
            var testData = new Tuple<Uri, string>(new Uri("http://www.cals.uidaho.edu/edComm/curricula/CustRel_curriculum/content/sample.pdf"),
                "Sample Adobe Reader File\n\nIf you can read this, you have Adobe Reader Installed.\n\n");

            var client = HelperFunctions.CreateWorkingClient();

            var response = client.ScanPdf(testData.Item1, Language.English);

            var result = response.Result;

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            Assert.IsTrue(result.Body != null);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Body.Text[0]));
            Assert.IsTrue(result.Body.Text[0] == testData.Item2);
        }
    }
}