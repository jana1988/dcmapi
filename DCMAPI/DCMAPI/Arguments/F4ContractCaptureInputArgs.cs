using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class F4ContractCaptureInputArgs
    {
        [DataMember]
        
        public int contract_id { get; set; }

        [DataMember]
        
        public string user_name { get; set; }

        [DataMember]
        public List<F4ContractCaptureDetail> f4detail { get; set; }
    }

    [DataContract]
    public class F4ContractCaptureDetail
    {
        [DataMember]
        
        public int id { get; set; }

        [DataMember]
        
        public decimal rate_value { get; set; }

        [DataMember]
        
        public string from_date { get; set; }

        [DataMember]
        
        public string to_date { get; set; }

        [DataMember]
        
        public string from_time { get; set; }

        [DataMember]
        
        public string to_time { get; set; }

        [DataMember]
        
        public string updated_on { get; set; }

        [DataMember]
        
        public string remarks { get; set; }
    }
}