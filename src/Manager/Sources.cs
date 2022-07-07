using System.Text.Json.Serialization;

public class Sources
{
    [JsonPropertyName("addresses")]
    public List<string> CidrAddress { get; set; }
}