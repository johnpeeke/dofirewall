using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.AddCommandLine(args);

var config = builder.Build();

var apiToken = config["apiToken"] ?? throw new Exception("apiToken is required.");
var ipList = config["ipList"] ?? throw new Exception("ipList is required.");
var firewallId = config["firewallId"] ?? throw new Exception("firewallId is required.");

var client = new HttpClient();
client.BaseAddress = new Uri("https://api.digitalocean.com/");
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
client.DefaultRequestHeaders.Add("ContentType", "application/json");

var request = await CreateFirewallRulesRequestAsync();

client.PostAsJsonAsync($"v2/firewalls/{firewallId}/rules", request).Result.EnsureSuccessStatusCode();

async Task<Root> CreateFirewallRulesRequestAsync()
{
    var sources = new Sources
    {
        CidrAddress = (await File.ReadAllLinesAsync(ipList)).ToList()
    };

    var inboundRule = new InboundRule
    {
        Protocol = "tcp",
        Ports = "80",
        Sources = sources
    };

    var root = new Root
    {
        InboundRules = new List<InboundRule> {inboundRule}
    };

    return root;
}