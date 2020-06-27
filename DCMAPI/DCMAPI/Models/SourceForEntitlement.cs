using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class SourceForEntitlement
    {
        [DataMember, Column("CONTRACT_ID")]
        public int contract_id { get; set; }

        [DataMember, Column("SOURCE")]
        public string source_name { get; set; }
    }
}