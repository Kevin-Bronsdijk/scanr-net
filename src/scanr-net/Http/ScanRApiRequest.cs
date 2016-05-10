using System;
using System.Collections.Generic;
using System.Net.Http;
using ScanR.Model;

namespace ScanR.Http
{
    internal class ScanRApiRequest : IApiRequest
    {
        public ScanRApiRequest(IRequest body, string uri)
        {
            Uri = uri;
            Method = HttpMethod.Post;
            Body = body;
        }

        public string Uri { get; set; }
        public HttpMethod Method { get; set; }
        public IRequest Body { get; set; }
        public List<Tuple<string, string>> QueryParameters { get; } = new List<Tuple<string, string>>();

        public void AddQueryParameter(string key, string value)
        {
            QueryParameters.Add(new Tuple<string, string>(key, value));
        }
    }
}