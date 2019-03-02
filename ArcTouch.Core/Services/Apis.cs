using Refit;
using System;
using System.Net.Http;

namespace ArcTouch.Core.Services
{
    public class Apis : IApis
    {
        public IIMDBApi IMDBApi { get; set; }
        public string IMDBApiKey { get => IMDBKey;}
        public const string IMDBKey = "1f54bd990f1cdfb230adb312546d765d";

        public Apis()
        {
            var client = new HttpClient() { BaseAddress = new Uri("https://api.themoviedb.org/3"), Timeout = new TimeSpan(0,0,3) };
            IMDBApi = RestService.For<IIMDBApi>(client);
        }
    }
}
