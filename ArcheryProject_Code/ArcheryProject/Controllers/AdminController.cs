using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Esf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;


namespace ArcheryProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private ArtaimusContext dbCtx;
        public List<PlayerModel> tmpPlayers = new List<PlayerModel>();

        public AdminController(ILogger<AdminController> logger, ArtaimusContext dbCtx)
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
                string[] tempLog = new string[] { "true" };
                match.PlayerIsLoggedIn = tempLog;

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
                match.ParcourId = match.Parcours.Id;
            }
            else
            {
                match.Parcours = dbCtx.Parcours.Where(x => x.Name == match.SelectedParcours).FirstOrDefault();
                match.ParcourId = match.Parcours.Id;
            }

            return View(match);
        }

        [HttpPost]
        public IActionResult ChangeParcour(EventModel match)
        {
            match.Parcours = dbCtx.Parcours.Where(x => x.Name == match.SelectedParcours).FirstOrDefault();
            match.ParcourId = match.Parcours.Id;
            return RedirectToAction("Play", "Admin", match);
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


        public IActionResult Admin()
        {
            List<ParcourModel> tmpModels = new List<ParcourModel>();

            foreach (var tmpPar in dbCtx.Parcours)
            {
                tmpModels.Add(new ParcourModel
                {
                    Id = tmpPar.Id,
                    Name = tmpPar.Name,
                    Location = tmpPar.Location,
                    CountAnimals = tmpPar.CountAnimals
                });
            }

            return View(tmpModels);
        }
        public IActionResult Admin2()
        {
            List<PlayerModel> tmpModels = new List<PlayerModel>();

            foreach (var tmpPar in dbCtx.Players)
            {
                tmpModels.Add(new PlayerModel
                {
                    Id = tmpPar.Id,
                    FirstName = tmpPar.FirstName,
                    LastName = tmpPar.LastName,
                    Nickname = tmpPar.Nickname,
                    Admin = tmpPar.Admin,
                });
            }

            return View(tmpModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost]
        public async Task<IActionResult> AddParcour(Parcour parcour)
        {
            if (ModelState.IsValid)
            {
                // Logik zum Speichern der Daten in der Datenbank
                dbCtx.Parcours.Add(parcour);
                await dbCtx.SaveChangesAsync();
            }
            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> EditParcour(Parcour parcour)
        {
            if (ModelState.IsValid)
            {
                // Logik zum Speichern der Daten in der Datenbank
                dbCtx.Parcours.Update(parcour);
                await dbCtx.SaveChangesAsync();
            }
            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> EditPlayer(Player player)
        {

                // Logik zum Speichern der Daten in der Datenbank
                dbCtx.Players.Update(player);
                await dbCtx.SaveChangesAsync();
            
            return RedirectToAction("Admin2");
        }
        [HttpPost]
        public IActionResult AddPlayerToPlay(PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                tmpPlayers.Add(player);
            }
            return View(tmpPlayers);
        }

        [HttpPost]
        public IActionResult PrintPlayerList(string matchType)
        {
            if(matchType == "3 Pfeil Wertung")            
            {
                foreach (var tmpPlayer in tmpPlayers)
                {
                    tmpPlayers.Add(new PlayerModel
                    {
                        Nickname = tmpPlayer.Nickname
                    });
                }
            }else
            {     

            }
                return View(matchType);
        }
    }
}