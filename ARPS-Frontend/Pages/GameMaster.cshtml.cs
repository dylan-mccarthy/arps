using ARPS.AILib.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARPS_Frontend.Pages
{
    public class GameMasterModel : PageModel
    {
        private readonly ILogger<GameMasterModel> _logger;
        private readonly GameMasterClient _gameMasterClient;

        public string ActionDescription = "";
        public GameMasterModel(ILogger<GameMasterModel> logger, GameMasterClient gameMasterClient)
        {
            _logger = logger;
            _gameMasterClient = gameMasterClient;
        }
        public async Task OnGet()
        {
            var response = await _gameMasterClient.GetActionDescription(new PlayerAction() { Description = "Test", Outcome = true });
            ActionDescription = response[0] ?? "No description available";
        }
    }
}