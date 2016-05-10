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

            //Assert.IsTrue(result.Success);
            //Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            //Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            //Assert.IsTrue(result.Body != null);
            //Assert.IsTrue(!string.IsNullOrEmpty(result.Body.Text));
            //Assert.IsTrue(result.Body.Text == "DevSlice.Net");
        }

        [TestMethod]
        public void Client_RequestUploadDevSliceNetFilePath_IsTrue()
        {
            var client = HelperFunctions.CreateWorkingClient();

            var response = client.Scan(
                TestData.LocalTestImage,
                Language.English);

            var result = response.Result;

            //Assert.IsTrue(result.Success);
           // Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            //Assert.IsTrue(string.IsNullOrEmpty(result.Error));
            //Assert.IsTrue(result.Body != null);
           // Assert.IsTrue(!string.IsNullOrEmpty(result.Body.Text));
            //Assert.IsTrue(result.Body.Text == "DevSlice.Net");
        }
    }
}