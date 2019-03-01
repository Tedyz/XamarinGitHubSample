using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcTouch.Core.Models
{
    public class PullRequest
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string PageUrl { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "user")]
        public Owner Owner { get; set; }

        public string CreatedAtFormated => CreatedAt.ToString("dd/MM/yyyy");
    }
}
