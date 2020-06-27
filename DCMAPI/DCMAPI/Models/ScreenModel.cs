using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class ScreenModel
    {
        [DataMember, Column("SCREEN_ID")]
        public int screen_id { get; set; }

        [DataMember, Column("SCREEN_NAME")]
        public string screen_name { get; set; }

        [DataMember, Column("SCREEN_URL")]
        public string screen_url { get; set; }
    }
}