using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Esf;
using System.Diagnostics;

namespace ArcheryProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private ArtaimusContext dbCtx;

        public AdminController(ILogger<AdminController> logger, ArtaimusContext dbCtx)
        {
            _logger = logger;

            this.dbCtx = dbCtx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Play()
        {
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
            List<ParcourModel> tmpMOdels = new List<ParcourModel>();

            foreach (var tmpPar in dbCtx.Parcours)
            {
                tmpMOdels.Add(new ParcourModel
                {
                    Id = tmpPar.Id,
                    Name = tmpPar.Name,
                    Location = tmpPar.Location,
                    CountAnimals = tmpPar.CountAnimals
                });
            }

            return View(tmpMOdels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class AdminModel
        {
            public string? Button { get; set; }
        }
    }
}