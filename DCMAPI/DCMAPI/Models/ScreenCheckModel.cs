using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class ScreenCheckModel
    {
        [DataMember, Column("view")]
        public bool view { get; set; }

        [DataMember, Column("add")]
        public bool add { get; set; }

        [DataMember, Column("edit")]
        public bool edit { get; set; }

        [DataMember, Column("delete")]
        public bool delete { get; set; }
    }
}