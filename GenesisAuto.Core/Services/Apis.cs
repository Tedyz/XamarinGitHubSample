using GenesisAuto.Core.Util;
using Refit;
using System;
using System.Net.Http;

namespace GenesisAuto.Core.Services
{
    public class Apis : IApis
    {
        public IGitHubApi GitHub { get ; set; }

        public Apis()
        {
            var client = new HttpClient(new UriQueryUnescapingHandler())
            {
                BaseAddress = new Uri("https://api.github.com"),
                Timeout = TimeSpan.FromSeconds(10)
            };
            GitHub = RestService.For<IGitHubApi>(client);
        }
    }
}
