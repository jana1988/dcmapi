using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Models
{
    [DataContract]
    public class F4ContractCaptureModel
    {
        [DataMember, Column("ID")]
        public int id { get; set; }

        [DataMember, Column("RATE_HEAD")]
        public string rate_head { get; set; }

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

        [DataMember, Column("UPDATED_ON")]
        public string updated_on { get; set; }

        [DataMember, Column("REMARKS")]
        public string remarks { get; set; }

        [DataMember, Column("RATE_UOM")]
        public string rate_uom { get; set; }
    }
}