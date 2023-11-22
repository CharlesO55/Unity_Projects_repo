using Newtonsoft.Json;
using System.Collections.Generic;

/* * * * * * * * * * * * * * * * * * * * * *
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 |      You may NOT edit this script.      |
 | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
 * * * * * * * * * * * * * * * * * * * * * */

public class DrawResponse {
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("deck_id")]
    public string DeckId { get; set; }

    [JsonProperty("cards")]
    public List<Card> Cards { get; set; }

    [JsonProperty("remaining")]
    public int Remaining { get; set; }
}
