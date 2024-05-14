using System.Text.Json;
using ARPS.AILib.Models;

public class GameMasterClient(HttpClient client){
    public async Task<List<string>?> GetSceneDescription(){
        var response = await client.GetAsync("/describe/scene");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(responseString);
    }
    public async Task<List<string>?> GetActionDescription(PlayerAction playerAction){
        var response = await client.PostAsJsonAsync("/describe/action", playerAction);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(responseString);
    }
}