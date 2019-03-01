using Refit;
using System;
using System.Net.Http;

namespace ArcTouch.Core.Services
{
    public class Apis : IApis
    {
        public IGitHubApi GitHub { get; set; }

        public Apis()
        {
            var client = new HttpClient();
            GitHub = RestService.For<IGitHubApi>(client);
        }
    }
}
