using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityProject.Models;

namespace NewUniversityProejct.Controllers
{


    public class HomeController : Controller
    {
        private GameCatalogDbContext db = new GameCatalogDbContext();

        public ActionResult Index(string nameSearch)
        {
            IQueryable<Game> game = db.Game.AsQueryable();

            ViewBag.NameSearch = nameSearch;
            if (!String.IsNullOrEmpty(nameSearch) && this.User.Identity.IsAuthenticated)
            {
                game = game.Where(x => x.Name.Contains(nameSearch));
            }

            return View(game);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Game1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

    }
}