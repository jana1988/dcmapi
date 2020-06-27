using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class RoleRightInputArgs
    {
        [DataMember]
        
        public int role_id { get; set; }

        [DataMember]
        
        public int menu_id { get; set; }

        [DataMember]
        
        public bool data_view { get; set; }

        [DataMember]
        
        public bool data_add { get; set; }

        [DataMember]
        
        public bool data_edit { get; set; }

        [DataMember]
        
        public bool data_delete { get; set; }

        [DataMember]
        
        public string user_name { get; set; }
    }

    [DataContract]
    public class UpdateRoleRightsArgs
    {
        [DataMember]
        
        public int role_id { get; set; }

        [DataMember]
        
        public List<RoleRightInputArgs> role_rights { get; set; }
    }
}