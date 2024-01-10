using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
            return View();
        }
        public IActionResult Admin()
        {
            List<ParcourModel> tmpModels = new List<ParcourModel>();

            foreach(var tmpPar in dbCtx.Parcours)
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

    }
}