using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaiseHand.Models
{
    //Location of student requesting help
    [Table("Location")]
    public class Location
    {
        //public int Id { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}