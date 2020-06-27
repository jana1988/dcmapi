using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Models
{
    [DataContract]
    public class F2ContractCaptureModel
    {
        [DataMember, Column("ID")]
        public int id { get; set; }

        [DataMember, Column("RATE_HEAD")]
        public string rate_head { get; set; }

        [DataMember, Column("APPLICABLE")]
        public bool applicable { get; set; }
    }
}