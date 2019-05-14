using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTruck.dto
{
    public class ReviewDTO
    {
        public int review_id { get; set; }
        public UserDTO users { get; set; }
        public FoodtruckReviewDTO foodtrucks { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
    }
}