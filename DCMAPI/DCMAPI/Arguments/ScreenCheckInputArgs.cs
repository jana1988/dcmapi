using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class ScreenCheckInputArgs
    {
        [DataMember]
        
        public int role_id { get; set; }

        [DataMember]
        
        public string screen_name { get; set; }
    }
}