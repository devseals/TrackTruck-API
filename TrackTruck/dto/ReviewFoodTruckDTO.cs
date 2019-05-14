using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTruck.dto
{
    public class ReviewFoodTruckDTO
    {
        public int review_id { get; set; }
        public UserDTO users { get; set; }
        public int foodtruck_id { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
    }
}