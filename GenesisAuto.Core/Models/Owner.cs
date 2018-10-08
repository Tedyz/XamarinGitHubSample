using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenesisAuto.Core.Models
{
    public class Owner
    {
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty(PropertyName = "login")]
        public string UserName { get; set; }
    }
}
