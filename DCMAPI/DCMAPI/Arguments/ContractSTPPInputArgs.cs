using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class ContractSTPPInputArgs
    {
        [DataMember]
        
        public int stpp_id { get; set; }

        [DataMember]
        
        public string type { get; set; }

        [DataMember]
        
        public string name { get; set; }
    }
}