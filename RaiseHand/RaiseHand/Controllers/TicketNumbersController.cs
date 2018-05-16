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
    public class TicketNumbersController : Controller
    {
        private RaisedHandEntities db = new RaisedHandEntities();

        // GET: TicketNumbers
        public ActionResult Index()
        {
            return View(db.TicketNumbers.ToList());
        }

        // GET: TicketNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNumber ticketNumber = db.TicketNumbers.Find(id);
            if (ticketNumber == null)
            {
                return HttpNotFound();
            }
            return View(ticketNumber);
        }

        // GET: TicketNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketNumber ticketNumber)
        {
            if (ModelState.IsValid)
            {
                db.TicketNumbers.Add(ticketNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketNumber);
        }

        // GET: TicketNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNumber ticketNumber = db.TicketNumbers.Find(id);
            if (ticketNumber == null)
            {
                return HttpNotFound();
            }
            return View(ticketNumber);
        }

        // POST: TicketNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketNumber ticketNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketNumber);
        }

        // GET: TicketNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNumber ticketNumber = db.TicketNumbers.Find(id);
            if (ticketNumber == null)
            {
                return HttpNotFound();
            }
            return View(ticketNumber);
        }

        // POST: TicketNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketNumber ticketNumber = db.TicketNumbers.Find(id);
            db.TicketNumbers.Remove(ticketNumber);
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
