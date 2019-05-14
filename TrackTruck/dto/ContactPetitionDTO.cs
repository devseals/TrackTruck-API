using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTruck.dto
{
    public class ContactPetitionDTO
    {
        public int contact_petition_id { get; set; }
        public OwnerDTO owners { get; set; }
        public UserDTO users { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }
        public string type { get; set; }
    }
}