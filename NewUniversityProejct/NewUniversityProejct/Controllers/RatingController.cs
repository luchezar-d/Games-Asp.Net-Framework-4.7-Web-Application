using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityProject.Models;

namespace NewUniversityProejct.Controllers
{
    public class RatingController : Controller
    {
        private GameCatalogDbContext db = new GameCatalogDbContext();
        // GET: Rating
        public ActionResult Index(string nameSearch, string sortOrder)
        {
            IQueryable<Rating> rating = db.Rating.AsQueryable();

            ViewBag.NameSearch = nameSearch;

            ViewBag.CurrentSortParm = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CreatedOn = sortOrder == "created_asc" ? "created_desc" : "created_asc";
            var a = rating.ToList();
            switch (sortOrder)
            {
                case "created_asc":
                    rating = rating.OrderBy(x => x.CreatedOn);
                    break;
                case "created_desc":
                    rating = rating.OrderByDescending(x => x.CreatedOn);
                    break;
            }
            return View(rating);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RatingValue,Description,CreatedOn")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Rating.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rating);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Rating.Find(id);
            db.Rating.Remove(rating);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RatingValue,Description")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rating);
        }
    }
}