using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class GeneratedScheduleModel
    {
        [DataMember, Column("SCHEDULE_REVISION")]
        public int revision { get; set; }

        [DataMember, Column("SCHEDULE_OUTPUT_JSON")]
        public string schedule_output_json { get; set; }

        [DataMember, Column("ENTITLEMENT_REVISION_JSON")]
        public string entitlement_revision_json { get; set; }

        [DataMember, Column("SYSTEM_DEMAND_REVISION_JSON")]
        public string system_demand_revision_json { get; set; }

        [DataMember, Column("IEX_REVISION_JSON")]
        public string iex_revision_json { get; set; }

        [DataMember, Column("REMC_REVISION_JSON")]
        public string remc_revision_json { get; set; }

        [DataMember, Column("ISFINAL_REVISION")]
        public bool isfinal_revision { get; set; }

        [DataMember, Column("REMARKS")]
        public string remarks { get; set; }
    }
}