using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripApplication.Models
{
    public class TripNotes
    {
        [Key]
        public int NoteID { get; set; }
        public string NoteName { get; set; }
    }
}