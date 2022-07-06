using System.Net.Http.Json;
using System.Text.Json.Serialization;

var client = new HttpClient();
client.BaseAddress = new Uri("https://api.digitalocean.com/");
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {args[0]}");
client.DefaultRequestHeaders.Add("ContentType", "application/json");

var sources = new Sources();
sources.CidrAddress = (await File.ReadAllLinesAsync("addresses.txt")).ToList();

var inboundRule = new InboundRule();
inboundRule.Protocol = "tcp";
inboundRule.Ports = "80";
inboundRule.Sources = sources;

var root = new Root();
root.InboundRules = new List<InboundRule>();
root.InboundRules.Add(inboundRule);

client.PostAsJsonAsync($"v2/firewalls/{args[1]}/rules", root).Result.EnsureSuccessStatusCode();

public class Root
{
    [JsonPropertyName("inbound_rules")]
    public List<InboundRule> InboundRules { get; set; }
}

public class InboundRule
{
    [JsonPropertyName("protocol")]
    public string Protocol { get; set; }

    [JsonPropertyName("ports")]
    public string Ports { get; set; }

    [JsonPropertyName("sources")]
    public Sources Sources { get; set; }
}

public class Sources
{
    [JsonPropertyName("addresses")]
    public List<string> CidrAddress { get; set; }
}