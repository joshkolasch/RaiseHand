using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Configuration;

namespace RaiseHand.Models
{
    public class RaisedHandEntities : DbContext
    {
        //AWSConnectionString
        //RaiseHandConnection
        public RaisedHandEntities()
        : base("name=RaiseHandConnection")
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ReasonLowered> ReasonsLowered { get; set; }

        public System.Data.Entity.DbSet<RaiseHand.Models.TicketNumber> TicketNumbers { get; set; }
    }
}