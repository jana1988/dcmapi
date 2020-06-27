using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class AddendumSourceModel
    {
        [DataMember, Column("CONTRACT_ID")]
        public int contract_id { get; set; }

        [DataMember, Column("SOURCE_NAME")]
        public string source_name { get; set; }
    }
}