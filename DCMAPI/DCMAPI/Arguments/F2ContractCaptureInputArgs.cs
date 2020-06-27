using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class F2ContractCaptureInputArgs
    {
        [DataMember]
        
        public int contract_id { get; set; }

        [DataMember]
        
        public string user_name { get; set; }

        [DataMember]
        public List<F2ContractCaptureDetail> f2detail { get; set; }
    }

    [DataContract]
    public class F2ContractCaptureDetail
    {
        [DataMember]
        
        public int id { get; set; }

        [DataMember]
        
        public bool applicable { get; set; }
    }
}