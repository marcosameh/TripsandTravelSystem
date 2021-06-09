using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripsandTravelSystem.Models
{
    public class TripPostVM
    {
        public int TripId { get; set; }
        [Required(ErrorMessage ="You Must Enter Trip Title")]
        [MinLength(3, ErrorMessage = "Short name")]
        [MaxLength(55, ErrorMessage = "invaild name")]
        public string TripTitle { get; set; }
        [Required(ErrorMessage = "You Must Enter Trip Details")]
        [MinLength(3, ErrorMessage = "Short name")]
        [MaxLength(700, ErrorMessage = "invaild name")]
        public string TripDetails { get; set; }
        public string PostDate { get; set; }
        [Required(ErrorMessage = "You Must Enter Trip Date")]
        public string TripDate { get; set; }
        [Required(ErrorMessage = "You Must Enter Trip Destination")]
        public string TripDestination { get; set; }
        [Required(ErrorMessage = "You Must Enter Trip Price")]
     

        public int Price { get; set; }
        public string TripImage { get; set; }
        public string Status { get; set; }
        public int AgencyId { get; set; }
        public String AgencyName { get; set; }
    public int NumOfLikes { get; set; }
    public int NumOfDislikes { get; set; }
    public int? Stars { get; set; }
        public bool? Like { get; set; }

        public bool? DisLike { get; set; }
        public int? Favourite { get; set; }
    }
}