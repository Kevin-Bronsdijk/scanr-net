using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ScanR.Model;
using System.Linq;

namespace ScanR.Http
{
    public class ScanRConnection : IDisposable
    {
        private readonly string _apiToken;
        private readonly Uri _apiUrl = new Uri("https://api.scanr.xyz/");
        private HttpClient _client;
        private JsonMediaTypeFormatter _formatter;
        
        internal ScanRConnection(string apiToken, HttpMessageHandler handler)
        {
            _client = new HttpClient(handler) {BaseAddress = _apiUrl };
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            ConfigureSerialization();

            _apiToken = apiToken;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void ConfigureSerialization()
        {
            _formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> {new StringEnumConverter {CamelCaseText = true}},
                    NullValueHandling = NullValueHandling.Ignore
                }
            };
        }

        public static ScanRConnection Create(string apiKey, IWebProxy proxy = null)
        {
            apiKey.ThrowIfNullOrEmpty("apiKey");

            var handler = new HttpClientHandler {Proxy = proxy};
            return new ScanRConnection(apiKey, handler);
        }

        private string BuildUrl(IApiRequest request)
        {
            var parameters = request.QueryParameters.Select( p => FormatQueryStringParameter(p.Item1, p.Item2)).ToList();

            parameters.Add(FormatQueryStringParameter("token", _apiToken));
            return "?" + string.Join("&", parameters);
        }

        private static string FormatQueryStringParameter(string key, string value)
        {
            return $"{Uri.EscapeUriString(key)}={WebUtility.UrlEncode(value)}";
        }

        internal async Task<IApiResponse<TResponse>> Execute<TResponse>(ScanRApiRequest scanRApiRequest, CancellationToken cancellationToken)
        {
            using (var requestMessage = new HttpRequestMessage(scanRApiRequest.Method, scanRApiRequest.Uri + BuildUrl(scanRApiRequest)))
            {
                requestMessage.Content = new ObjectContent(scanRApiRequest.Body.GetType(),
                    scanRApiRequest.Body, _formatter, new MediaTypeHeaderValue("application/json"));

                using (
                    var responseMessage =
                        await _client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false))
                {
                    var test = await responseMessage.Content.ReadAsStringAsync();

                    return await BuildResponse<TResponse>(responseMessage, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        internal async Task<IApiResponse<TResponse>> ExecuteUpload<TResponse>(
            ScanRApiRequest scanRApiRequest, byte[] image, string filename, CancellationToken cancellationToken)
        {
            filename.ThrowIfNullOrEmpty("filename");

            using (
                var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
            {
                content.Add(new StreamContent(new MemoryStream(image)), "file", filename);

                using (
                    var responseMessage =
                        await _client.PostAsync(_apiUrl + scanRApiRequest.Uri + BuildUrl(scanRApiRequest), content, cancellationToken))
                {
                    //var test = await responseMessage.Content.ReadAsStringAsync();

                    return await BuildResponse<TResponse>(responseMessage, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private async Task<IApiResponse<TResponse>> BuildResponse<TResponse>(HttpResponseMessage message,
            CancellationToken cancellationToken)
        {
            var response = new ApiResponse<TResponse>
            {
                StatusCode = message.StatusCode,
                Success = message.IsSuccessStatusCode
            };

            if (message.Content != null)
            {
                if (message.IsSuccessStatusCode)
                {
                    try
                    {
                        response.Body =
                            await
                                message.Content.ReadAsAsync<TResponse>(new[] { _formatter }, cancellationToken)
                                    .ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Cannot serialize the content", ex);
                    }
                }
                else
                {
                    try
                    {
                        var errorResponse = await message.Content.ReadAsAsync<ErrorResult>(cancellationToken).ConfigureAwait(false);

                        if (errorResponse != null)
                        {
                            response.Error = errorResponse.Error;
                        }
                    }
                    catch (Exception)
                    {
                        response.Error = "Cannot serialize the content";
                    }
  
                }
            }

            return response;
        }

        ~ScanRConnection()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }
        }
    }
}