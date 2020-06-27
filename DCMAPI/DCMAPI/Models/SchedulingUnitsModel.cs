using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class SchedulingUnitsModel
    {
        [DataMember, Column("CONTRACT_UNIT_WISE_SCHEDULING")]
        public bool IsUnitWiseScheduling { get; set; }

        [DataMember, Column("CONTRACT_NO_OF_UNITS")]
        public int units { get; set; }
    }
}