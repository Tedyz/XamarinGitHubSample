using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcTouch.Core.Models
{
    public class Repository
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty(PropertyName = "forks_count")]
        public int ForksCount { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public Owner Owner { get; set; }
    }
}
