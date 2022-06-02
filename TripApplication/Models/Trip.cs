using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripApplication.Models
{
    public class Trip
    {
        [Key]
        public int TripID { get; set; }
        public string TripName { get; set;}
        public string TripCountry { get; set; }
        public DateTime TripDate { get; set; }

        //A note belongs to a trip
        [ForeignKey("TripNotes")]
        public int NoteID { get; set; }
        public virtual TripNotes TripNotes { get; set; }    
    }

    public class TripDto
    {
        public int TripID { get; set; }
        public string TripName { get; set; }
        public string TripCountry { get; set; }
        public DateTime TripDate { get; set; }
       
        public string TripNotes { get; set; }

    }
}