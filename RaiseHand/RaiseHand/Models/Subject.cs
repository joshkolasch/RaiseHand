﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaiseHand.Models
{
    [Table("Subject")]
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}