using Newtonsoft.Json;

namespace PR2Macro.Models
{
    public class PR2User
    {
        [JsonProperty("error")]
        public string? Error { get; set; }
        [JsonProperty("userId")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("group")]
        public int Group { get; set; }
        [JsonProperty("trial_mod")]
        public string? Trial { get; set; }
        [JsonProperty("guildId")]
        public int GuildId { get; set; }
        [JsonProperty("guildName")]
        public string? GuildName { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
        [JsonProperty("hats")]
        public int Hats { get; set; }
        [JsonProperty("registerDate")]
        public string? RegisterDate { get; set; }
        [JsonProperty("loginDate")]
        public string? LoginDate { get; set; }
        [JsonProperty("hat")]
        public int Hat { get; set; }
        [JsonProperty("head")]
        public int Head { get; set; }
        [JsonProperty("body")]
        public int Body { get; set; }
        [JsonProperty("feet")]
        public int Feet { get; set; }
        [JsonProperty("hatColor")]
        public int HatColor { get; set; }
        [JsonProperty("headColor")]
        public int HeadColor { get; set; }
        [JsonProperty("bodyColor")]
        public int BodyColor { get; set; }
        [JsonProperty("feetColor")]
        public int FeetColor { get; set; }
        [JsonProperty("hatColor2")]
        public int HatColor2 { get; set; }
        [JsonProperty("headColor2")]
        public int HeadColor2 { get; set; }
        [JsonProperty("bodyColor2")]
        public int BodyColor2 { get; set; }
        [JsonProperty("feetColor2")]
        public int FeetColor2 { get; set; }
        [JsonProperty("exp_points")]
        public int ExpPoints { get; set; }
        [JsonProperty("exp_to_rank")]
        public int ExpToRank { get; set; }
        [JsonProperty("following")]
        public int Following { get; set; }
        [JsonProperty("friend")]
        public int Friend { get; set; }
        [JsonProperty("ignored")]
        public int Ignored { get; set; }

    }
}
