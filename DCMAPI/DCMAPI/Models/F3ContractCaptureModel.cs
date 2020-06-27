using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace DCMAPI.Models
{
    [DataContract]
    public class F3ContractCaptureModel
    {
        [DataMember, Column("ADDENDUM_ID")]
        public int addendum_id { get; set; }

        [DataMember, Column("FROM_DATE")]
        public string from_date { get; set; }

        [DataMember, Column("TO_DATE")]
        public string to_date { get; set; }

        [DataMember, Column("FROM_TIME")]
        public string from_time { get; set; }

        [DataMember, Column("TO_TIME")]
        public string to_time { get; set; }

        [DataMember, Column("CONTRACTED_CAPACITY")]
        public decimal contracted_capacity { get; set; }

        [DataMember, Column("OA_CAPACITY")]
        public decimal oa_capacity { get; set; }

        [DataMember, Column("CONTRACTED_RATE")]
        public string contracted_rate { get; set; }

        [DataMember, Column("DISCOUNT")]
        public decimal discount { get; set; }
    }
}