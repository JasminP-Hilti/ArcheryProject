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
            if (player == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Parcour[] ParcourArray = dbCtx.Parcours.ToArray();
            match.ParcourArr = new string[ParcourArray.Length];
            for (var pos = 0; pos < ParcourArray.Length; pos++)
            {
                Parcour tmpP = ParcourArray[pos];
                match.ParcourArr[pos] = tmpP.Name;
            }



            if (match.PlayerListArr == null)
            {

                if (player.Nickname != null)
                {
                    match.PlayerList.Add(player.Nickname);
                }
                else
                {
                    match.PlayerList.Add(player.Email);
                }
                match.LoggedIn.Add(true);

            }
            if (match.PlayerListArr != null)
            {
                foreach (var entry in match.PlayerListArr)
                {
                    match.PlayerList.Add(entry);
                }
            }
            if (match.SelectedParcours == null)
            {
                match.Parcours = dbCtx.Parcours.Where(x => x.Name == match.ParcourArr[0]).FirstOrDefault();
            }
            else
            {
                match.Parcours = dbCtx.Parcours.Where(x => x.Name == match.SelectedParcours).FirstOrDefault();
            }

            return View(match);
        }

        [HttpPost]
        public IActionResult ChangeParcour(EventModel match)
        {
            match.Parcours = dbCtx.Parcours.Where(x => x.Name == match.SelectedParcours).FirstOrDefault();
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
