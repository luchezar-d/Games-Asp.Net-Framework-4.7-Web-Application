using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityProject.Models;
using PagedList;
using System.Data.Entity;
using NewUniversityProejct.Models;

namespace UniversityProject.Controllers
{
    public class GameController : Controller
    {
        private GameCatalogDbContext db = new GameCatalogDbContext();
        // GET: Default
        public ActionResult Index(int? page, string nameSearch, string sortOrder)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<Game> game = db.Game.AsQueryable();

            ViewBag.NameSearch = nameSearch;
            if (!String.IsNullOrEmpty(nameSearch) && this.User.Identity.IsAuthenticated)
            {
                game = game.Where(x => x.Name.Contains(nameSearch));
            }

            ViewBag.CurrentSortParm = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GenreSortParm = sortOrder == "genre_asc" ? "genre_desc" : "genre_asc";
            ViewBag.RatingSortParm = sortOrder == "rating_asc" ? "rating_desc" : "rating_asc";
            var a = game.ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    game = game.OrderByDescending(x => x.Name);
                    break;
                case "genre_asc":
                    game = game.OrderBy(x => x.Genre.GenreName);
                    break;
                case "genre_desc":
                    game = game.OrderByDescending(x => x.Genre.GenreName);
                    break;
                case "rating_asc":
                    game = game.OrderBy(x => x.Rating.RatingValue);
                    break;
                case "rating_desc":
                    game = game.OrderByDescending(x => x.Rating.RatingValue);
                    break;
                default:
                    game = game.OrderBy(x => x.Name);
                    break;
            }

            return View(game.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            ViewBag.Rating = new SelectList(db.Rating, "Id", "RatingValue");
            ViewBag.Genre = new SelectList(db.Genre, "Id", "GenreName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {
            //[Bind(Include = "Id,Name,ReleaseYear,GenreId,RatingId,")] 
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rating = new SelectList(db.Rating, "Id", "RatingValue");
            ViewBag.Genre = new SelectList(db.Genre, "Id", "GenreName");

            return View(game);
        }
        public ActionResult Details(int? id)
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
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

            ViewBag.Rating = db.Rating;
            ViewBag.Genre = db.Genre;

            return View(game);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Game model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model != null)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rating = db.Rating;
            ViewBag.Genre = db.Genre;

            return View(model);
        }
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}