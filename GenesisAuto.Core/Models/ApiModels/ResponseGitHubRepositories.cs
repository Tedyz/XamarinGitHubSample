using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenesisAuto.Core.Models.Api
{
    public class ResponseGitHubRepositories
    {
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "incomplete_results")]
        public string IncompleteResults { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<Repository> Items { get; set; }
    }
}
