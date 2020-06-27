using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class EntitlementArgs
    {
        [DataMember]
        
        public string date { get; set; }

        [DataMember]
        
        public string remarks { get; set; }

        [DataMember]
        
        public int revision { get; set; }

        [DataMember]
        
        public string type { get; set; }

        [DataMember]
        
        public string source { get; set; }
    }
}