using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class F1ContractCaptureModel
    {
        [DataMember, Column("CONTRACT_ID")]
        public int contract_Id { get; set; }

        [DataMember, Column("CONTRACT_TYPE")]
        public string contract_type { get; set; }

        [DataMember, Column("CONTRACT_PPA_ID")]
        public int ppa { get; set; }

        [DataMember, Column("CONTRACT_SOURCE_ID")]
        public int source_id { get; set; }

        [DataMember, Column("CONTRACT_TRADER_ID")]
        public int trader_id { get; set; }

        [DataMember, Column("CONTRACT_PURCHASER_ID")]
        public int purchaser_id { get; set; }

        [DataMember, Column("CONTRACT_CONSUMER_TYPE")]
        public string open_access { get; set; }

        [DataMember, Column("CONTRACT_CA_NO")]
        public string ca_no { get; set; }

        [DataMember, Column("CONTRACT_CONSUMER_NAME")]
        public string consumer_name { get; set; }

        [DataMember, Column("CONTRACT_DATE_OF_CONTRACT")]
        public string dt_of_contract { get; set; }

        [DataMember, Column("CONTRACT_OPEN_ACCESS_TYPE")]
        public string open_access_type { get; set; }

        [DataMember, Column("CONTRACT_TYPE1")]
        public string type1 { get; set; }

        [DataMember, Column("CONTRACT_TYPE2")]
        public string type_2 { get; set; }

        [DataMember, Column("CONTRACT_TYPE3")]
        public string type_3 { get; set; }

        [DataMember, Column("CONTRACT_SOURCE")]
        public string source { get; set; }

        [DataMember, Column("CONTRACT_UNIT_WISE_SCHEDULING")]
        public bool unit_wise_schedule { get; set; }

        [DataMember, Column("CONTRACT_NO_OF_UNITS")]
        public int no_of_units { get; set; }

        [DataMember, Column("CONTRACT_DELIVERY_POINT")]
        public string delivery_point { get; set; }

        [DataMember, Column("CONTRACT_EXCEPTION")]
        public string exception_dt_description { get; set; }

        [DataMember, Column("CONTRACT_MIN_ENERGY_OFFTAKE")]
        public decimal min_energy_per { get; set; }

        [DataMember, Column("CONTRACT_MIN_CAPACITY_OFFTAKE")]
        public decimal min_capacity_per { get; set; }

        [DataMember, Column("CONTRACT_AVAILABILITY_FOR_FC")]
        public decimal availability_per { get; set; }

        [DataMember, Column("CONTRACT_PENALTY_RATE")]
        public decimal penalty_rate { get; set; }

        [DataMember, Column("CONTRACT_RAMP_UP_RATE")]
        public decimal ramp_up_rate { get; set; }

        [DataMember, Column("CONTRACT_RAMP_DOWN_RATE")]
        public decimal ramp_down_rate { get; set; }

        [DataMember, Column("CONTRACT_MOD_APPLICABILITY")]
        public bool mod_applicability { get; set; }

        [DataMember, Column("CONTRACT_AUX_APPLICABILITY")]
        public bool aux_applicability { get; set; }

        [DataMember, Column("CONTRACT_REMC_SCHEDULING_ENABLED")]
        public bool remc_scheduling { get; set; }

        [DataMember, Column("CONTRACT_PEAK_OFFPEAK")]
        public bool peak_offpeak { get; set; }

        [DataMember, Column("CREATED_BY")]
        public string user_name { get; set; }

        [DataMember, Column("CONTRACT_LOI_NO")]
        public string contract_loi_no { get; set; }

        [DataMember]
        public List<UnitDetailsModel> unit_details { get; set; }
    }

    public class UnitDetailsModel
    {
        [DataMember, Column("UNIT_CAPACITY")]
        public decimal capacity { get; set; }

        [DataMember, Column("UNIT_MINIMUM")]
        public decimal minimum { get; set; }
    }
}