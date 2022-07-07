using System.Text.Json.Serialization;

public class Root
{
    [JsonPropertyName("inbound_rules")]
    public List<InboundRule> InboundRules { get; set; }
}