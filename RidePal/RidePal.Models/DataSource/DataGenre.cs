using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Models.DataSource
{
    public class DataGenre
    {
        [JsonProperty("data")]
        public List<Genre> Genres { get; set; }
    }
}
