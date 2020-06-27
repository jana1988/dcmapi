using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class MenuModel
    {
        [DataMember, Column("role_id")]
        public int role_id { get; set; }

        [DataMember, Column("menu_id")]
        public int menu_id { get; set; }

        [DataMember, Column("menu_name")]
        public string menu_name { get; set; }

        [DataMember, Column("menu_link")]
        public string menu_link { get; set; }

        [DataMember, Column("menu_parent")]
        public int menu_parent { get; set; }

        [DataMember, Column("data_view")]
        public bool data_view { get; set; }

        [DataMember, Column("data_add")]
        public bool data_add { get; set; }

        [DataMember, Column("data_edit")]
        public bool data_edit { get; set; }

        [DataMember, Column("data_delete")]
        public bool data_delete { get; set; }

        [DataMember, Column("menu_item")]
        public bool menu_item { get; set; }

        [DataMember]
        public IEnumerable<MenuModel> items { get; set; }

        public bool ShouldSerializeitems()
        {
            return items.Any();
        }
    }
}