using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class UserInputArgs
    {
        [DataMember]
        
        public string created_by { get; set; }

        [DataMember]
        
        public string user_name { get; set; }

        [DataMember]
        
        public int role_id { get; set; }

        [DataMember]
        
        public bool is_internal { get; set; }

        [DataMember]
        
        public string password { get; set; }

        [DataMember]
        
        public string first_name { get; set; }

        [DataMember]
        
        public string middle_name { get; set; }

        [DataMember]
        
        public string last_name { get; set; }

        [DataMember]
        
        public string complete_name { get; set; }

        [DataMember]
        
        public string job_title { get; set; }

        [DataMember]
        
        public string email { get; set; }

        [DataMember]
        
        public string mobile { get; set; }

        [DataMember]
        
        public string user_title { get; set; }

        //public List<UserDetailInputArgs> data { get; set; }
    }
}