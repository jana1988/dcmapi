using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class ContractSTPPModel
    {
        [DataMember]
        public List<STPP> source { get; set; }

        [DataMember]
        public List<STPP> trade { get; set; }

        [DataMember]
        public List<STPP> purchase { get; set; }

        [DataMember]
        public List<STPP> ppa { get; set; }
    }

    [DataContract]
    public class STPP
    {
        [DataMember, Column("ID")]
        public int id { get; set; }

        [DataMember, Column("NAME")]
        public string name { get; set; }
    }

    [DataContract]
    public class ContractSTPPBindModel
    {
        [DataMember, Column("ID")]
        public int id { get; set; }

        [DataMember, Column("TYPE")]
        public string type { get; set; }


        [DataMember, Column("NAME")]
        public string name { get; set; }
    }
}