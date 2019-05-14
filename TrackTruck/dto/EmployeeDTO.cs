using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTruck.dto
{
    public class EmployeeDTO
    {
        public int employee_id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        //public string password { get; set; }
        public OwnerDTO owners { get; set; }
    }
}