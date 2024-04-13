﻿using Newtonsoft.Json;

namespace PR2Macro.Models
{
    public class PR2ServerList
    {
        public PR2ServerList(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("servers")]
        public List<PR2Server>? Servers { get; set; }
    }
}
