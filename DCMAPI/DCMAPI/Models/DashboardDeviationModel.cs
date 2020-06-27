using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class DashboardDeviationModel
    {
        [DataMember, Column("BLOCKNO")]
        public int TimeBlock { get; set; }

        [DataMember, Column("AVGDEVIATIONMW")]
        public decimal Deviation { get; set; }

        [DataMember, Column("AVGUIRATE")]
        public decimal Rate { get; set; }
    }


    public class DeviationModel
    {
        public int[] timeblock { get; set; }
        public decimal[] deviation { get; set; }
        public decimal[] rate { get; set; }
    }

}