﻿using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class GetContractDetailArgs
    {
        [DataMember]
        
        public int contract_id { get; set; }
    }
}