using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaiseHand.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int LocationId { get; set; }
        public int SubjectId { get; set; }
        public int StatusId { get; set; }
        public int Number { get; set; }
        public DateTime TimeRaised { get; set; }
        public DateTime? TimeLowered { get; set; }
        public int? ReasonLoweredId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Status Status { get; set; }
        public virtual ReasonLowered ReasonLowered { get; set; }
    }
}