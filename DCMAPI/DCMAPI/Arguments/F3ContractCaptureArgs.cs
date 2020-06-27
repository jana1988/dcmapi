using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class F3ContractCaptureArgs
    {
        [DataMember]
        
        public string user_name { get; set; }

        [DataMember]
        
        public int contract_id { get; set; }

        [DataMember]
        public List<F3ContractCaptureDetail> f3detail { get; set; }
    }

    [DataContract]
    public class F3ContractCaptureDetail
    {
        [DataMember]
        
        public int addendum_id { get; set; }

        [DataMember]
        
        public string from_date { get; set; }

        [DataMember]
        
        public string to_date { get; set; }

        [DataMember]
        
        public string from_time { get; set; }

        [DataMember]
        
        public string to_time { get; set; }

        [DataMember]
        
        public decimal contracted_capacity { get; set; }

        [DataMember]
        
        public decimal oa_capacity { get; set; }

        [DataMember]
        
        public string contracted_rate { get; set; }

        [DataMember]
        
        public decimal discount { get; set; }
    }
}