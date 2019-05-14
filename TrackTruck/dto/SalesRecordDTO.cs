using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackTruck.dto
{
    public class SalesRecordDTO
    {
        public int sales_record_id { get; set; }
        public EmployeeDTO employees { get; set; }
        public float value { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }
    }
}