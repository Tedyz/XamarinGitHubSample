using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcTouch.Core.Models.Api
{
    public class ResponseIMDBMoviesList
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<MovieItem> Results { get; set; }
    }
}
