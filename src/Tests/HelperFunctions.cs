using ScanR;
using ScanR.Http;

namespace Tests
{
    public static class HelperFunctions
    {
        public static ScanRClient CreateWorkingClient(bool debug = false)
        {
            var connection = ScanRConnection.Create(Settings.ApiToken);
            var client = new ScanRClient(connection);

            return client;
        }
    }
}