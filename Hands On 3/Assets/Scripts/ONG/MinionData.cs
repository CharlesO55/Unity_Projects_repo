using Newtonsoft.Json;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                    This script has [TODO]'s.                    |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    
/* Provided code, do NOT remove. */
[JsonObject(MemberSerialization.OptIn)]
public class MinionData {
    /* [TODO] : Fill-up as needed by the [WebAPIManager]. */

    /* Provided code, do NOT remove. */
    [JsonProperty("health")]
    public int Health;
    [JsonProperty("type")]
    public string Type;

    [JsonProperty("img")]
    public string Img;

    [JsonProperty("cost")]
    public int Cost;

    [JsonProperty("attack")]
    public int Attack;

    [JsonProperty("cardId")]
    public string CardID;
}
