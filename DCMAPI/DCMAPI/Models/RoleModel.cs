using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class RoleModel
    {
        [DataMember, Column("ROLE_ID")]
        public int role_id { get; set; }

        [DataMember, Column("ROLE_NAME")]
        public string role_name { get; set; }
    }
}