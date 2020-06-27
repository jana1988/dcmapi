using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class RTDChart1Model
    {
        [DataMember, Column("TIMEBLOCK")]
        public int Timeblock { get; set; }

        [DataMember, Column("FORECAST_DEMAND")]
        public decimal Forecast_Demand { get; set; }

        [DataMember, Column("ACTUAL_DEMAND")]
        public decimal Actual_Demand { get; set; }

        [DataMember, Column("SCHEDULED_POWER")]
        public decimal Scheduled_Power { get; set; }

        [DataMember, Column("AVAILABLE_GENERATION")]
        public decimal Available_Generation { get; set; }
    }

    public class RTDChart1
    {
        public int[] Timeblock { get; set; }

        public decimal[] Forecast_Demand { get; set; }

        public decimal[] Actual_Demand { get; set; }

        public decimal[] Scheduled_Power { get; set; }

        public decimal[] Available_Generation { get; set; }
    }
}