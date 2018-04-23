﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaiseHand.Models
{
    //Reason ticket was resolved
    [Table("ReasonLowered")]
    public class ReasonLowered
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}