using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember, Column("USER_TYPE")]
        public bool is_internal { get; set; }

        [DataMember, Column("USER_LOGINID")]
        public string user_name { get; set; }

        [DataMember, Column("USER_ROLE")]
        public int role_id { get; set; }

        [DataMember, Column("USER_SN")]
        public string last_name { get; set; }

        [DataMember, Column("USER_GIVENNAME")]
        public string first_name { get; set; }

        [DataMember, Column("USER_MIDDLENAME")]
        public string midle_name { get; set; }

        [DataMember, Column("USER_JOBTITLE")]
        public string job_title { get; set; }

        [DataMember, Column("USER_EMAIL")]
        public string email { get; set; }

        [DataMember, Column("USER_TITLE")]
        public string user_title { get; set; }

        [DataMember, Column("USER_MOBILE")]
        public string mobile { get; set; }

        [DataMember, Column("CREADTE_ON")]
        public DateTime created_on { get; set; }

        [DataMember, Column("CREADTE_BY")]
        public string created_by { get; set; }
     
    }
}