using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryProject.Controllers
{
    public class PlayerController : Controller
    {
        private ArtaimusContext dbCtx;
        public IActionResult Index(PlayerModel player)
        {
            return View(player);
        }
        public IActionResult Play()
        {
            //todo: get player from login info
            return View();
        }
        public IActionResult Stats()
        {
            List<StatisticModel> tmpModels = new List<StatisticModel>();

            var playersList = dbCtx.Players.ToList();
            var parcourList = dbCtx.Parcours.ToList();
            var eventList = dbCtx.Events.ToList();

            foreach (var eventPlayer in dbCtx.EventsHasPlayers)
            {
                var player = playersList.FirstOrDefault(p => p.Id == eventPlayer.PlayersId /*&& p.Id ==2*/);
                var eventInfo = eventList.FirstOrDefault(e => e.Id == eventPlayer.EventsId);



                if (player != null && eventInfo != null)
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
