using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class SLDCScheduleModel
    {

        [DataMember, Column("FROMTIME")]
        public string FromTime { get; set; }

        [DataMember, Column("TOTIME")]
        public string ToTime { get; set; }

        [DataMember, Column("TIMEBLOCK")]
        public int TimeBlock { get; set; }

        [DataMember, Column("DEMAND_FORECAST_GT")]
        public decimal Demand_Forecast_GT { get; set; }

        [DataMember, Column("TARGET_DRAWAL_SCHEDULE_TD")]
        public decimal Target_Drawal_Schedule_TD { get; set; }

        [DataMember, Column("TARGET_DRAWAL_SCHEDULE_GT")]
        public decimal Target_Drawal_Schedule_GT { get; set; }

        [DataMember, Column("DTPS_U1_SCHEDULE")]
        public decimal DTPS_U1_Schedule { get; set; }

        [DataMember, Column("DTPS_U2_SCHEDULE")]
        public decimal DTPS_U2_Schedule { get; set; }

        [DataMember, Column("BILATERAL_SOURCE_1")]
        public decimal Bilateral_Source_1 { get; set; }

        [DataMember, Column("BILATERAL_SOURCE_2")]
        public decimal Bilateral_Source_2 { get; set; }

        [DataMember, Column("CONTRACTED_RE_1")]
        public decimal Contracted_RE_1 { get; set; }

        [DataMember, Column("CONTRACTED_RE_2")]
        public decimal Contracted_RE_2 { get; set; }

        [DataMember, Column("STANDBY")]
        public decimal Standby { get; set; }

        [DataMember, Column("FIRM_OA")]
        public decimal Firm_OA { get; set; }

        [DataMember, Column("IEX_PURCHASE")]
        public decimal IEX_Purchase { get; set; }

        [DataMember, Column("IEX_SALE")]
        public decimal IEX_Sale { get; set; }

        [DataMember, Column("PX_PURCHASE")]
        public decimal PX_Purchase { get; set; }

        [DataMember, Column("PX_SALE")]
        public decimal PX_Sale { get; set; }

        [DataMember, Column("RTM_PURCHASE")]
        public decimal RTM_Purchase { get; set; }

        [DataMember, Column("RTM_SALE")]
        public decimal RTM_Sale { get; set; }

        [DataMember, Column("OTHER_OA_RE_NON_SOLAR")]
        public decimal OTHER_OA_RE_NON_Solar { get; set; }

        [DataMember, Column("OTHER_OA_RE_SOLAR")]
        public decimal OTHER_OA_RE_Solar { get; set; }

        [DataMember, Column("REMC_CONTRACTED_WIND")]
        public decimal REMC_Contracted_Wind { get; set; }

        [DataMember, Column("REMC_CONTRACTED_SOLAR")]
        public decimal REMC_Contracted_Solar { get; set; }

        [DataMember, Column("REMC_OA_NON_SOLAR")]
        public decimal REMC_OA_NON_Solar { get; set; }

        [DataMember, Column("REMC_OA_SOLAR")]
        public decimal REMC_OA_Solar { get; set; }

        [DataMember, Column("OTHER_1")]
        public decimal Other_1 { get; set; }

        [DataMember, Column("OTHER_2")]
        public decimal Other_2 { get; set; }

        [DataMember, Column("TOTAL")]
        public decimal Total { get; set; }

        [DataMember, Column("SHORTFALL")]
        public decimal Shortfall { get; set; }

        [DataMember, Column("SURPLUS")]
        public decimal Surplus { get; set; }
    }

    public class SLDCUploadedModel
    {
       
        [DataMember, Column("REVISION")]
        public int revision { get; set; }
    }
}