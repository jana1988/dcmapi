using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class SchedulingRevisionModel
    {
        [DataMember, Column("R_ID")]
        public int r_id { get; set; }

        [DataMember, Column("SOURCES")]
        public string source { get; set; }

        [DataMember, Column("Revision")]
        public int revision { get; set; }
    }
    public class SchedulingRevision
    {
        [DataMember, Column("Revision")]
        public string revision { get; set; }
    }

    public class GenetatedSchedulingRevision
    {
        [DataMember, Column("ID")]
        public int r_id { get; set; }

        [DataMember, Column("Revision")]
        public string revision { get; set; }
    }
}