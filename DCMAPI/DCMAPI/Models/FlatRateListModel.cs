using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class FlatRateListModel
    {
        [DataMember, Column("RATE_ID")]
        public int rate_id { get; set; }

        [DataMember, Column("RATE_HEAD")]
        public string rate_head { get; set; }
    }
}