using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class RoleInputArgs
    {
        [DataMember]
        
        public int role_id { get; set; }

        [DataMember]
        
        public int role_active { get; set; }

        [DataMember]
        
        public string role_name { get; set; }

        [DataMember]
       
        public string user_name { get; set; }
    }
}