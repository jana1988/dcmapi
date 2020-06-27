using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class UserDetails
    {
        [DataMember, Column("user_name")]
        public string user_name { get; set; }

        [DataMember, Column("is_internal")]
        public bool is_internal { get; set; }

        [DataMember, Column("role_id")]
        public int role_id { get; set; }

        [DataMember, Column("full_name")]
        public string full_name { get; set; }

        [DataMember, Column("first_name")]
        public string first_name { get; set; }

        [DataMember, Column("middle_name")]
        public string middle_name { get; set; }

        [DataMember, Column("last_name")]
        public string last_name { get; set; }

        [DataMember, Column("job_title")]
        public string job_title { get; set; }

        [DataMember, Column("email")]
        public string email { get; set; }

        [DataMember, Column("mobile")]
        public string mobile { get; set; }

        [DataMember, Column("post_code")]
        public string post_code { get; set; }
    }

    [DataContract]
    public class LoggedInUserModel
    {
        [DataMember]
        public string user_name { get; set; }

        [DataMember]
        public bool is_internal { get; set; }
        
        [DataMember]
        public int role_id { get; set; }

        [DataMember]
        public string full_name { get; set; }

        [DataMember]
        public string first_name { get; set; }

        [DataMember]
        public string middle_name { get; set; }

        [DataMember]
        public string last_name { get; set; }

        [DataMember]
        public string job_title { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string mobile { get; set; }

        [DataMember]
        public string post_code { get; set; }
    }
}