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
    public class TicketsController : Controller
    {
        private RaisedHandEntities db = new RaisedHandEntities();

        private static int ticketNumberIndex = 0;
        private string[] ticketNumbers = { "A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3" };
        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Location).Include(t => t.ReasonLowered).Include(t => t.Status).Include(t => t.Subject);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.ReasonLoweredId = new SelectList(db.ReasonsLowered, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationId,SubjectId,StatusId,Number,TimeRaised,TimeLowered,ReasonLoweredId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.ReasonLoweredId = new SelectList(db.ReasonsLowered, "Id", "Name", ticket.ReasonLoweredId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", ticket.StatusId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.ReasonLoweredId = new SelectList(db.ReasonsLowered, "Id", "Name", ticket.ReasonLoweredId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", ticket.StatusId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationId,SubjectId,StatusId,Number,TimeRaised,TimeLowered,ReasonLoweredId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.ReasonLoweredId = new SelectList(db.ReasonsLowered, "Id", "Name", ticket.ReasonLoweredId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", ticket.StatusId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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

        // GET: Tickets/Create
        public ActionResult Home()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Home([Bind(Include = "Id,LocationId,SubjectId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //Generate the following below: TicketId, StatusId, Number, TimeRaised
                ticket.Number = GenerateTicketNumber();
                var status = db.Statuses
                                .Where(x => x.Name == "Active")
                                .First();
                
                ticket.StatusId = status.Id;
                var time = DateTime.Now;
                ticket.TimeRaised = time;


                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("HandRaised", new { id = ticket.Id });
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }

        //GET:Tickets/HandRaised/5
        public ActionResult HandRaised(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
            if (ticket.StatusId == inactiveStatus)
            {
                //Ticket has been rendered inactive.
                //TODO: consider outputting a message that says this ticket has been removed?
                return HttpNotFound();
            }

            var totalHandsRaised = CountHandsRaised();
            ViewBag.HandCount = totalHandsRaised;
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);

            return View(ticket);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandRaised([Bind(Include = "LocationId, SubjectId")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldticket = db.Tickets.Find(ticket.Id);

                //User has changed their location or subject
                if(oldticket.LocationId != ticket.LocationId || oldticket.SubjectId != ticket.SubjectId)
                {
                    ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
                    ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);

                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                //important status codes
                //TODO: these should probably be kept as static values so that the database isn't constantly queried everytime a hand is raised or lowered.
                var helpedStatus = db.ReasonsLowered.Where(x => x.Name == "StudentSelectedHelped").First().Id;
                var nevermindStatus = db.ReasonsLowered.Where(x => x.Name == "StudentSelectedNevermind").First().Id;
                var unhelpedStatus = db.ReasonsLowered.Where(x => x.Name == "StudentSelectedUnhelped").First().Id;
                var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
                var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
                 
                //Student is trying to lower their hand
                if (ticket.ReasonLoweredId == helpedStatus || ticket.ReasonLoweredId == nevermindStatus || ticket.ReasonLoweredId == unhelpedStatus)
                {
                    //if the student's hand hasn't been lowered already by the tutor
                    if(oldticket.StatusId == activeStatus)
                    {
                        ticket.StatusId = inactiveStatus;
                        ticket.TimeLowered = DateTime.Now;
                    }

                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();

                    //No feedback necessary if they figured out their own question
                    if(ticket.ReasonLoweredId == nevermindStatus)
                    {
                        return RedirectToAction("Home");
                    }

                    //Feedback desired for satisfied and unsatisfied students
                    return RedirectToAction("Feedback", "HomeController");
                }
                
                //if the student had their hand lowered by a tutor when they still had a question
                if (oldticket.StatusId != activeStatus)
                {
                    return RedirectToAction("HandLowered", new { id = ticket.Id });
                }
                
                return View(ticket);
            }

            //if invalid modelstate
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }
        */

        // GET: Tickets/Details/5
        public ActionResult HandLowered(int? id)                        
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //TODO: I couldn't get this to run as an HttpPost. Should I be doing this as a post? There is no form necessary. Also, for Nevermind() and Unhelped() actionlinks.
        public ActionResult Helped(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            //TODO: store helpedStatus as a static variable
            var helpedReasonId = db.ReasonsLowered.Where(x => x.Name == "StudentSelectedHelped").First().Id;
            var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            var oldticket = db.Tickets.Find(ticket.Id);

            if (oldticket.StatusId == activeStatus)
            {
                ticket.ReasonLoweredId = helpedReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.Now;
            }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Feedback", "Home");
        }
        

        public ActionResult Nevermind(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var nevermindReasonId = db.ReasonsLowered.Where(x => x.Name == "StudentSelectedHelped").First().Id;
            var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            var oldticket = db.Tickets.Find(ticket.Id);

            if (oldticket.StatusId == activeStatus)
            {
                ticket.ReasonLoweredId = nevermindReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.Now;
            }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Home");
        }


        public ActionResult Unhelped(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var unhelpedReasonId = db.ReasonsLowered.Where(x => x.Name == "StudentSelectedHelped").First().Id;
            var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            var oldticket = db.Tickets.Find(ticket.Id);

            if (oldticket.StatusId == activeStatus)
            {
                ticket.ReasonLoweredId = unhelpedReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.Now;
            }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Feedback", "Home");
        }


        //TODO:This currently isn't functioning. I think the TicketId is null :( because the oldticket object is null
        //I should probably change this so that I'm just sending the Id and passing a new LocationId through the ViewBag.
        //I would probably have to compare the ViewBag.NewLocationId with the ticket.LocationId before sending it to the server to prevent a senseless transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLocation([Bind(Include = "LocationId")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldticket = db.Tickets.Find(ticket.Id);
                var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
                
                if (oldticket.LocationId != ticket.LocationId)
                {
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();

                    //if user's ticket is still active, update their browser
                    if(oldticket.StatusId == activeStatus)
                    {
                        return RedirectToAction("HandRaised", new { id = ticket.Id });
                    }
                    //if user's ticket has been taken down for some reason, have them verify the status of their ticket.
                    else
                    {
                        return RedirectToAction("HandLowered", new { id = ticket.Id });
                    }

                }
                else
                {
                    //TODO: Send a message saying "change your location please"
                    //Note: this should probably already be handled client-side
                }
            }

            //Invalid State, return the ticket to the student
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }

        //TODO:This currently isn't functioning. I think the TicketId is null :( because the oldticket object is null
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeSubject([Bind(Include = "SubjectId")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldticket = db.Tickets.Find(ticket.Id);
                var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;

                if (oldticket.SubjectId != ticket.SubjectId)
                {
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();

                    //if user's ticket is still active, update their browser
                    if (oldticket.StatusId == activeStatus)
                    {
                        return RedirectToAction("HandRaised", new { id = ticket.Id });
                    }
                    //if user's ticket has been taken down for some reason, have them verify the status of their ticket.
                    else
                    {
                        return RedirectToAction("HandLowered", new { id = ticket.Id });
                    }

                }
                else
                {
                    //TODO: Send a message saying "change your location please"
                    //Note: this should probably already be handled client-side
                }
            }

            //Invalid State, return the ticket to the student
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);
            return View(ticket);
        }

        public ActionResult TicketManager()
        {
            var activeStatus = db.Statuses.Where(s => s.Name == "Active").First().Id;
            var allTickets = db.Tickets.Include(t => t.Location).Include(t => t.ReasonLowered).Include(t => t.Status).Include(t => t.Subject);
            var activeTickets = allTickets.Where(t => t.StatusId == activeStatus).OrderBy(t => t.TimeRaised).OrderBy(t => t.Number);

            return View(activeTickets.ToList());
        }

        //Tutor selected 'Helped' from the TicketManager page
        //Update the database if necessary and refresh the TicketManager page
        public ActionResult TutorHelped(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var tutorHelpedReasonId = db.ReasonsLowered.Where(x => x.Name == "TutorSelectedHelped").First().Id;
            var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            var oldticket = db.Tickets.Find(ticket.Id);

            if (oldticket.StatusId == activeStatus)
            {
                ticket.ReasonLoweredId = tutorHelpedReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.Now;
            }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TicketManager", "Tickets");
        }

        //Tutor selected 'Not Found' from the TicketManager page
        //Update the database if necessary and refresh the TicketManager page
        public ActionResult NotFound(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var notFoundReasonId = db.ReasonsLowered.Where(x => x.Name == "TutorSelectedNotFound").First().Id;
            var inactiveStatus = db.Statuses.Where(x => x.Name == "Inactive").First().Id;
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            var oldticket = db.Tickets.Find(ticket.Id);

            if (oldticket.StatusId == activeStatus)
            {
                ticket.ReasonLoweredId = notFoundReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.Now;
            }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TicketManager", "Tickets");
        }

        //TODO: This method should be replaced with a better method of generating/selecting ticket numbers to issue to students
        public string GenerateTicketNumber()
        {
            string number;
            number = ticketNumbers[ticketNumberIndex];
            ticketNumberIndex++;
            if(ticketNumberIndex > ticketNumbers.Length)
            {
                ticketNumberIndex = 0;
            }

            return number;
        }

        public int CountHandsRaised()
        {
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            int count = db.Tickets.Where(x => x.StatusId == activeStatus).Count();

            return count;
        }
    }
}
