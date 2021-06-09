using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsandTravelSystem.Models
{
    public class RatePostsVM
    {
        public int RateId { get; set; }
        public Nullable<bool> Like { get; set; }
        public Nullable<bool> DisLike { get; set; }
        public Nullable<int> Stars { get; set; }
        public int TravalerId { get; set; }
        public int PostId { get; set; }
    }
}