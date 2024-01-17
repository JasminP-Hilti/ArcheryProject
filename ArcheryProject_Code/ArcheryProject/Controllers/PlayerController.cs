using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryProject.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ILogger<PlayerController> _logger;

        private ArtaimusContext dbCtx;
        public List<PlayerModel> tmpPlayers = new List<PlayerModel>();

        public PlayerController(ILogger<PlayerController> logger, ArtaimusContext dbCtx)
        {
            _logger = logger;

            this.dbCtx = dbCtx;
        }

        public IActionResult Index(PlayerModel player)
        {
            return View(player);
        }

        public IActionResult Play(PlayerModel player)
        {
            //Get logged in Player from DB
            return View(player);
        }
        public IActionResult Stats(PlayerModel player)
        {
            return View(player);
        }
    }
}
