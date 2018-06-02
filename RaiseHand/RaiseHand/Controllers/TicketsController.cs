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

        //private static int ticketNumberIndex = 0;
        //private string[] ticketNumbers = { "A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3" };
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
                //Note:this is the GenerateTicketNumber 1.0
                //ticket.Number = GenerateTicketNumber();
                //GenerateTicketNumber 2.0
                ticket.Number = GenerateTicketNumber(db.Tickets.Count());
                var status = db.Statuses
                                .Where(x => x.Name == "Active")
                                .First();
                
                ticket.StatusId = status.Id;
                DateTime universalTime = DateTime.UtcNow;

                var time = universalTime;
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
            //subtract 1 from the handcount to exclude the users hand.
            ViewBag.HandCount = totalHandsRaised - 1;
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", ticket.LocationId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", ticket.SubjectId);

            return View(ticket);
        }
        

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
                ticket.TimeLowered = DateTime.UtcNow;
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
                ticket.TimeLowered = DateTime.UtcNow;
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
                ticket.TimeLowered = DateTime.UtcNow;
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
        public ActionResult ChangeLocation([Bind(Include = "Id,LocationId")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldticket = db.Tickets.Find(ticket.Id);
                var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
                
                if (oldticket.LocationId != ticket.LocationId)
                {
                    //Note: there should be a check here to prevent SQL injection
                    oldticket.LocationId = ticket.LocationId;
                    db.Entry(oldticket).State = EntityState.Modified;
                    db.SaveChanges();

                    //if user's ticket is still active, update their browser
                    if(oldticket.StatusId == activeStatus)
                    {
                        return RedirectToAction("HandRaised", new { id = oldticket.Id });
                    }
                    //if user's ticket has been taken down for some reason, have them verify the status of their ticket.
                    else
                    {
                        return RedirectToAction("HandLowered", new { id = oldticket.Id });
                    }

                }
                else
                {
                    //TODO: Send a message saying "change your location please"
                    //Note: this should probably already be handled client-side
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //TODO:This currently isn't functioning. I think the TicketId is null :( because the oldticket object is null
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeSubject([Bind(Include = "Id,LocationId,SubjectId")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldticket = db.Tickets.Find(ticket.Id);
                var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;

                if (oldticket.SubjectId != ticket.SubjectId)
                {
                    //Note: there should be a check here to make sure that the SubjectId is not a SQL injection.
                    oldticket.SubjectId = ticket.SubjectId;
                    db.Entry(oldticket).State = EntityState.Modified;
                    db.SaveChanges();

                    //if user's ticket is still active, update their browser
                    if (oldticket.StatusId == activeStatus)
                    {
                        return RedirectToAction("HandRaised", new { id = oldticket.Id });
                    }
                    //if user's ticket has been taken down for some reason, have them verify the status of their ticket.
                    else
                    {
                        return RedirectToAction("HandLowered", new { id = oldticket.Id });
                    }

                }
                else
                {
                    //TODO: Send a message saying "change your location please"
                    //TODO: Also, there should be a check to see if their status is still 'active'
                    //Note: this should probably already be handled client-side
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult TicketManager()
        {
            var activeStatus = db.Statuses.Where(s => s.Name == "Active").First().Id;
            var allTickets = db.Tickets.Include(t => t.Location).Include(t => t.ReasonLowered).Include(t => t.Status).Include(t => t.Subject);
            var activeTickets = allTickets.Where(t => t.StatusId == activeStatus).OrderBy(t => t.TimeRaised);

            try
            {
                var pacificTimeTickets = GetPacificTime(activeTickets);
                return View(pacificTimeTickets.ToList());
            }
            catch (NullReferenceException e)
            {
                //TODO: this should either return an error, or return a more descriptive page than HttpNotFound
                //It should fail gracefully if the function doesn't work properly.
                return HttpNotFound();
            }
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
            //var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            //var oldticket = db.Tickets.Find(ticket.Id);

            if (ticket.StatusId != inactiveStatus)
            {
                ticket.ReasonLoweredId = tutorHelpedReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.UtcNow;
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
            //var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            //var oldticket = db.Tickets.Find(ticket.Id);

            if (ticket.StatusId != inactiveStatus)
            {
                ticket.ReasonLoweredId = notFoundReasonId;
                ticket.StatusId = inactiveStatus;
                ticket.TimeLowered = DateTime.UtcNow;
            }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TicketManager", "Tickets");
        }

        //Version 1.0
        //TODO: This method should be replaced with a better method of generating/selecting ticket numbers to issue to students
        /*public string GenerateTicketNumber()
        {
            string number;
            number = ticketNumbers[ticketNumberIndex];
            ticketNumberIndex++;
            if(ticketNumberIndex > ticketNumbers.Length)
            {
                ticketNumberIndex = 0;
            }

            return number;
        }*/

        //Version 2.0
        //Note: TicketNumbers table starts with an id of 1 (not 0)
        public string GenerateTicketNumber(int id)
        {
            string number;
            int count = db.TicketNumbers.Count();

            int locateId = (id % count) + 1;

            number = db.TicketNumbers.Find(locateId).Name.ToString();

            return number;
        }

        public int CountHandsRaised()
        {
            var activeStatus = db.Statuses.Where(x => x.Name == "Active").First().Id;
            int count = db.Tickets.Where(x => x.StatusId == activeStatus).Count();

            return count;
        }

        public IOrderedQueryable<Ticket> GetPacificTime(IOrderedQueryable<Ticket> tickets)
        {
            if(tickets == null)
            {
                return null;
            }

            string pacificTimezoneId = "Pacific Standard Time";
            
            foreach (var ticket in tickets)
            {
                var pacificTime = TimeZoneInfo.ConvertTimeFromUtc(ticket.TimeRaised, TimeZoneInfo.FindSystemTimeZoneById(pacificTimezoneId));
                ticket.TimeRaised = pacificTime;
            }
            return tickets;
        }
    }
}
