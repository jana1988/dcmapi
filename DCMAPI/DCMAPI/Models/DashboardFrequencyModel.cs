using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class DashboardFrequencyModel
    {
        [DataMember, Column("BLOCKNO")]
        public int TimeBlock { get; set; }

        [DataMember, Column("AVGFREQ")]
        public decimal Frequency { get; set; }

        [DataMember, Column("AVGUIRATE")]
        public decimal Rate { get; set; }
    }


    public class FrequencyModel
    {
        public int[] timeblock { get; set; }
        public decimal[] frequency { get; set; }
        public decimal[] rate { get; set; }
    }

}