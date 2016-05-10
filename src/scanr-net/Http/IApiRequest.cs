using System;
using System.Collections.Generic;
using System.Net.Http;
using ScanR.Model;

namespace ScanR.Http
{
    internal interface IApiRequest
    {
        string Uri { get; set; }
        IRequest Body { get; set; }
        HttpMethod Method { get; set; }
        List<Tuple<string, string>> QueryParameters { get;  }
        void AddQueryParameter(string key, string value);
    }
}