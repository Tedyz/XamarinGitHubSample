using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenesisAuto.Core.Util
{
    public class UriQueryUnescapingHandler : DelegatingHandler
    {
        public UriQueryUnescapingHandler()
            : base(new HttpClientHandler()) { }
        public UriQueryUnescapingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        { }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri;
            //You could also simply unescape the whole uri.OriginalString
            //but i don´t recommend that, i.e only fix what´s broken
            var unescapedQuery = Uri.UnescapeDataString(uri.Query);

            var userInfo = string.IsNullOrWhiteSpace(uri.UserInfo) ? "" : $"{uri.UserInfo}@";
            var scheme = string.IsNullOrWhiteSpace(uri.Scheme) ? "" : $"{uri.Scheme}://";

            request.RequestUri = new Uri($"{scheme}{userInfo}{uri.Authority}{uri.AbsolutePath}{unescapedQuery}{uri.Fragment}");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
