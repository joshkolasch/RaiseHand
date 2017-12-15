using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaiseHand.Models
{
    [Table("Location")]
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}