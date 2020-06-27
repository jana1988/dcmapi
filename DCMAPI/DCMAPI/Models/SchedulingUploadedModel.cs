using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class SchedulingUploadedModel
    {
        [DataMember, Column("TYPE")]
        public string type { get; set; }

        [DataMember, Column("DATETIME")]
        public string datetime { get; set; }

        [DataMember, Column("REVISION")]
        public int revision { get; set; }
    }
}