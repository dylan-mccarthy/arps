using System.Text.Json;
using ARPS.AILib.Models;

public class ProbabilityCalculatorClient(HttpClient client){
    public async Task<PlayerAction?> GetProbability(PlayerAction playerAction){
        var response = await client.PostAsJsonAsync("/calculate", playerAction);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var playerActionResponse = JsonSerializer.Deserialize<PlayerAction>(responseString);
        return playerActionResponse;
    }
}