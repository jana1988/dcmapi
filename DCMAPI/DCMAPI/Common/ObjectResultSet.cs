using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Common
{
    [DataContract]
    public class ObjectResultSet<T>
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public T data { get; set; }
    }
}