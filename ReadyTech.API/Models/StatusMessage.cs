using System.Text.Json.Serialization;

namespace ReadyTech.API.Models;

public class StatusMessage
{
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("prepared")]
    public string Prepared { get; set; }
}
