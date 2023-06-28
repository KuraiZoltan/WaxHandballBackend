using Angular_Test_App.Model.Players;
using Angular_Test_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Angular_Test_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("getPlayers")]
        public IEnumerable<Player> GetPlayers()
        {
            return _playerService.GetPlayers();
        }

        [HttpPost]
        [Route("addPlayer")]
        public async Task AddPlayer([FromBody] Player player)
        {
            await _playerService.AddPlayer(player);
        }
    }
}
