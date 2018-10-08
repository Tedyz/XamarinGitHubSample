using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GenesisAuto.Core.Services
{
    public class Apis : IApis
    {
        public IGitHubApi GitHub { get ; set; }

        public Apis()
        {
            GitHub = RestService.For<IGitHubApi>(new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com"),
                Timeout = TimeSpan.FromSeconds(10)
            });
        }
    }
}
