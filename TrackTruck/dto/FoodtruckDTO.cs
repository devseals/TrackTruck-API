using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTruck.dto
{
    public class FoodtruckDTO
    {
        public int foodtruck_id { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string food_type { get; set; }
        public float avg_price { get; set; }
        public OwnerDTO owners { get; set; }
        public List<ReviewFoodTruckDTO> Reviews { get; set; }
    }
}