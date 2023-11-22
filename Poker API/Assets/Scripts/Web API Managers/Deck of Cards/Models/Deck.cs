
/* * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |     You SHOULD edit this script. :)     |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * */

using Newtonsoft.Json;

public class Deck {
    [JsonProperty("success")]
    public bool Success { get; set; }
    [JsonProperty("deck_id")]
    public string Deck_ID { get; set; }
    [JsonProperty("remaining")]
    public int Remaining { get; set; }
    [JsonProperty("shuffled")]
    public bool Shuffled { get; set; }

}
