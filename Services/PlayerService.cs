using Angular_Test_App.Model;
using Angular_Test_App.Model.Players;

namespace Angular_Test_App.Services
{
    public class PlayerService
    {
        private readonly WaxHandballAppDbContext _context;

        public PlayerService(WaxHandballAppDbContext context)
        {
            _context = context;
        }

        public List<Player> GetPlayers()
        {
            var palyers = _context.Players.ToList();
            return palyers;
        }

        public async Task AddPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }
    }
}
