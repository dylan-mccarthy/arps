using System.Text.Json;
using ARPS.AILib.Models;

public class ProbabilityCalculatorClient(HttpClient client){
    public async Task<List<string>?> GetProbability(PlayerAction playerAction){
        var response = await client.PostAsJsonAsync("/probability", playerAction);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(responseString);
    }
}