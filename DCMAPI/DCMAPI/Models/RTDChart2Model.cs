using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    public class RTDChart2Model
    {
        [DataMember, Column("BLOCKNO")]
        public int BlockNo { get; set; }

        [DataMember, Column("DEVIATION")]
        public decimal Deviation { get; set; }

        [DataMember, Column("FIRST_LIMIT")]
        public decimal First_Limit { get; set; }

        [DataMember, Column("INITIAL_LIMIT")]
        public decimal InitiaL_Limit { get; set; }

        [DataMember, Column("SECOND_LIMIT")]
        public decimal Second_Limit { get; set; }
    }

    public class RTDChart2
    {
        public int[] BlockNo { get; set; }

        public decimal[] Deviation { get; set; }

        public decimal[] First_Limit { get; set; }

        public decimal[] InitiaL_Limit { get; set; }

        public decimal[] Second_Limit { get; set; }
    }
}