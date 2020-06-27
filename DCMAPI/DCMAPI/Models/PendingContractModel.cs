using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class PendingContractModel
    {
        [DataMember, Column("CONTRACT_ID")]
        public int contract_id { get; set; }

        [DataMember, Column("CONTRACT_TYPE")]
        public string contract_type { get; set; }

        [DataMember, Column("SOURCE")]
        public string contract_source { get; set; }

        [DataMember, Column("CONTRACT_OPEN_ACCESS_TYPE")]
        public string contract_entitle_type { get; set; }
    }
}