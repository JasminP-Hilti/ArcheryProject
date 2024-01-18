using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryProject.Controllers
{
    public class PlayerController : Controller
    {
        private ArtaimusContext dbCtx;
        private readonly ILogger<PlayerController> _logger;
        public List<PlayerModel> tmpPlayers = new List<PlayerModel>();

        public PlayerController(ILogger<PlayerController> logger, ArtaimusContext dbCtx)
        {
            _logger = logger;

            this.dbCtx = dbCtx;
        }


        public IActionResult Index()
        {
            PlayerModel player = ApiHelper.GetUser(HttpContext.Session.GetString("UsernameOrEmail"));
            return View(player);
        }

        [HttpGet]
        public IActionResult Play(EventModel match)
        {

            PlayerModel player = ApiHelper.GetUser(HttpContext.Session.GetString("UsernameOrEmail"));
            if (match.PlayerList.Count == 0)
            {
                match = new EventModel();
                if (player.Nickname != null)
                {
                    match.PlayerList.Add(player.Nickname);
                }
                else
                {
                    match.PlayerList.Add(player.Email);
                }
                match.LoggedIn.Add(true);
                List<Parcour> ParcourList = dbCtx.Parcours.ToList();
                match.Parcours = ParcourList[1];
                match.Name = "New Game";
            }

            return View(match);
        }


        [HttpPost]
        public IActionResult PlaySetup(EventModel match, bool loggedIn)
        {
            return RedirectToAction("Play", "Player", match);
        }



        public IActionResult Logout()
        {
            var usernameOrEmail = HttpContext.Session.GetString("UsernameOrEmail");

            if (usernameOrEmail != null)
            {
                PlayerModel player = ApiHelper.GetUser(usernameOrEmail);
                ApiHelper.RemoveUser(player);

                HttpContext.Session.Clear();
            }

            return RedirectToAction("index", "Home");

        }


        public IActionResult Stats()
        {
            PlayerModel player = ApiHelper.GetUser(HttpContext.Session.GetString("UsernameOrEmail"));

            List<StatisticModel> tmpModels = new List<StatisticModel>();

            var playersList = dbCtx.Players.ToList();
            var parcourList = dbCtx.Parcours.ToList();
            var eventList = dbCtx.Events.ToList();

            foreach (var eventPlayer in dbCtx.EventsHasPlayers)
            {
                var tmpplayer = playersList.FirstOrDefault(p => p.Id == eventPlayer.PlayersId && p.Id == player.Id);
                var eventInfo = eventList.FirstOrDefault(e => e.Id == eventPlayer.EventsId);



                if (tmpplayer != null && eventInfo != null)
                {
                    var parcour = parcourList.FirstOrDefault(p => p.Id == eventInfo.ParcoursId);

                    if (parcour != null)
                    {
                        tmpModels.Add(new StatisticModel
                        {
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            Nickname = player.Nickname,
                            PlayersId = eventPlayer.PlayersId,
                            EventsId = eventPlayer.EventsId,
                            PointsTotal = eventPlayer.PointsTotal,
                            ParcoursName = parcour.Name,
                            ParcoursId = parcour.Id,
                            ParcoursCountAnimals = parcour.CountAnimals

                        });
                    }
                }
            }

            return View(tmpModels);



        }

    }

}
