using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RaiseHand.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        public int Id { get; set; }
        //public string StudentId { get; set; }
        //where the student is sitting
        [Required]
        [Display(Name = "Location")]
        public string LocationId { get; set; }
        //subject student needs help with
        [Required]
        [Display(Name ="Subject")]
        public int SubjectId { get; set; }
        //Ticket status (active or inactive)
        public int StatusId { get; set; }
        //Ticket number displayed to students and tutors
        public string Number { get; set; }
        //Time ticket issued
        public DateTime TimeRaised { get; set; }
        //Time ticket resolved
        public DateTime? TimeLowered { get; set; }
        //Indicates how the ticket was resolved
        //Important for data collection on whether the question was answered or not.
        public int? ReasonLoweredId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Status Status { get; set; }
        public virtual ReasonLowered ReasonLowered { get; set; }
    }
}