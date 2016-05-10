using System.Net;

namespace ScanR.Http
{
    public interface IApiResponse
    {
        bool Success { get; }
        HttpStatusCode StatusCode { get; }
        string Error { get; }
    }
}