using System.Text.Json.Serialization;

public class InboundRule
{
    [JsonPropertyName("protocol")]
    public string Protocol { get; set; }

    [JsonPropertyName("ports")]
    public string Ports { get; set; }

    [JsonPropertyName("sources")]
    public Sources Sources { get; set; }
}