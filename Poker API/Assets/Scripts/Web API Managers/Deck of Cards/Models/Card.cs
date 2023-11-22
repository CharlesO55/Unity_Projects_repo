
/* * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |     You SHOULD edit this script. :)     |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * */

using Newtonsoft.Json;
using System.Collections.Generic;

public class Card {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("images")]
    public Dictionary<string, string> Images { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("suit")]
    public string Suit { get; set; }
}
