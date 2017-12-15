using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaiseHand.Models;

namespace RaiseHand.Controllers
{
    public class ReasonLoweredsController : Controller
    {
        private RaisedHandEntities db = new RaisedHandEntities();

        // GET: ReasonLowereds
        public ActionResult Index()
        {
            return View(db.ReasonsLowered.ToList());
        }

        // GET: ReasonLowereds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReasonLowered reasonLowered = db.ReasonsLowered.Find(id);
            if (reasonLowered == null)
            {
                return HttpNotFound();
            }
            return View(reasonLowered);
        }

        // GET: ReasonLowereds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReasonLowereds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ReasonLowered reasonLowered)
        {
            if (ModelState.IsValid)
            {
                db.ReasonsLowered.Add(reasonLowered);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reasonLowered);
        }

        // GET: ReasonLowereds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReasonLowered reasonLowered = db.ReasonsLowered.Find(id);
            if (reasonLowered == null)
            {
                return HttpNotFound();
            }
            return View(reasonLowered);
        }

        // POST: ReasonLowereds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ReasonLowered reasonLowered)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reasonLowered).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reasonLowered);
        }

        // GET: ReasonLowereds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReasonLowered reasonLowered = db.ReasonsLowered.Find(id);
            if (reasonLowered == null)
            {
                return HttpNotFound();
            }
            return View(reasonLowered);
        }

        // POST: ReasonLowereds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReasonLowered reasonLowered = db.ReasonsLowered.Find(id);
            db.ReasonsLowered.Remove(reasonLowered);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
