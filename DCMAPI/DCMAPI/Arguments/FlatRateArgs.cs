using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class FlatRateArgs
    {
        [DataMember]
        
        public int id { get; set; }

        [DataMember]
        
        public int rate_id { get; set; }

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
        
        public string remarks { get; set; }

        [DataMember]
        
        public string updated_By { get; set; }
    }
}