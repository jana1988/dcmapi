using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class FlateRateModel
    {
        [DataMember, Column("ID")]
        public int f_id { get; set; }

        [DataMember, Column("RATE_ID")]
        public int rate_id { get; set; }

        [DataMember, Column("RATE_VALUE")]
        public decimal rate_value { get; set; }

        [DataMember, Column("FROM_DATE")]
        public string from_date { get; set; }

        [DataMember, Column("TO_DATE")]
        public string to_date { get; set; }

        [DataMember, Column("FROM_TIME")]
        public string from_time { get; set; }

        [DataMember, Column("TO_TIME")]
        public string to_time { get; set; }

        [DataMember, Column("REMARKS")]
        public string remarks { get; set; }

        [DataMember, Column("UPDATED_ON")]
        public string updated_on { get; set; }

        [DataMember, Column("CREATED_ON")]
        public string created_on { get; set; }

        [DataMember, Column("CREATED_BY")]
        public string created_by { get; set; }

        [DataMember, Column("EDITED_ON")]
        public string edited_on { get; set; }

        [DataMember, Column("EDITED_BY")]
        public string edited_by { get; set; }

        [DataMember, Column("RATE_HEAD")]
        public string rate_head { get; set; }

        [DataMember, Column("RATE_UOM")]
        public string rate_uom { get; set; }

        [DataMember, Column("RATE_INTERVAL")]
        public string rate_interval { get; set; }

    }
}