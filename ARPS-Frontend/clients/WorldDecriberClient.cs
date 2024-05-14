using System.Text.Json;
using ARPS.AILib.Models;

public class WorldDescriberClient(HttpClient client){
    public async Task<List<string>?> GetDescription(PlayerAction playerAction){
        var response = await client.PostAsJsonAsync("/description", playerAction);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(responseString);
    }
}