using Newtonsoft.Json;

namespace PR2Macro.Models
{
    public class PR2Server
    {
        [JsonProperty("server_id")]
        public int Id { get; set; }

        [JsonProperty("server_name")]
        public string? Name { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("guild_id")]
        public int GuildId { get; set; }

        [JsonProperty("tournament")]
        public int Tournament { get; set; }

        [JsonProperty("happy_hour")]
        public int HappyHour { get; set; }

        public override string ToString()
        {
            return Status != null && Status.Equals("down", StringComparison.InvariantCultureIgnoreCase) && Id < 12
                ? Name + " (down)"
                : HappyHour == 1 && Id < 12
                    ? "!! " + Name + " (" + Population + " online)"
                    : HappyHour == 1 && Id > 11
                                    ? "* !! " + Name + " (" + Population + " online)"
                                    : Status != null && Status.Equals("down", StringComparison.InvariantCultureIgnoreCase) && Id > 10
                                                    ? "* " + Name + " (down)"
                                                    : Id > 11 ? "* " + Name + " (" + Population + " online)" : Name + " (" + Population + " online)";
        }
    }
}
