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

        public IActionResult Index(PlayerModel player)
        {

            return View(player);
        }

        public IActionResult Play()
        {
            //Get logged in Player from DB
            return View();
        }

        public IActionResult Stats()
        {

            List<StatisticModel> tmpModels = new List<StatisticModel>();

            var playersList = dbCtx.Players.ToList();
            var parcourList = dbCtx.Parcours.ToList();
            var eventList= dbCtx.Events.ToList();

            foreach (var eventPlayer in dbCtx.EventsHasPlayers)
            {
                var player = playersList.FirstOrDefault(p => p.Id == eventPlayer.PlayersId /*&& p.Id ==2*/);
                var eventInfo = eventList.FirstOrDefault(e => e.Id == eventPlayer.EventsId);



                if (player != null && eventInfo != null)
                {
                    var parcour = parcourList.FirstOrDefault(p => p.Id == eventInfo.ParcoursId);

                    if(parcour != null)
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
                        ParcoursId = parcour.Id
                                             
                    });
                    }
                }
            }

            return View(tmpModels);

            

            //List<EventHasPlayerModel> tmpModels = new List<EventHasPlayerModel>();

            //foreach (var tmpPoints in dbCtx.EventsHasPlayers)
            //{
            //    foreach (var player in dbCtx.Players)
            //    {

            //    }

            //    tmpModels.Add(new EventHasPlayerModel
            //    {
            //        PlayersId = tmpPoints.PlayersId,
            //        EventsId = tmpPoints.EventsId,
            //        PointsTotal = tmpPoints.PointsTotal

            //    });

            //}

            //return View(tmpModels);


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
        public IActionResult AddPlayerToPlay(PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                tmpPlayers.Add(player);
            }
            return View(tmpPlayers);
        }

    }
}