using System.Text.Json.Serialization;

namespace ARPS.AILib.Models;
public class PlayerAction
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    public bool Outcome { get; set; }
}