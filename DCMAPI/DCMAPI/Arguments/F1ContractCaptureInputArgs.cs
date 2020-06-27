using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class F1ContractCaptureInputArgs
    {
        [DataMember]
        
        public int contract_Id { get; set; }

        [DataMember]
        
        public string contract_type { get; set; }

        [DataMember]
        
        public int ppa { get; set; }

        [DataMember]
        
        public int source_id { get; set; }

        [DataMember]
        
        public int trader_id { get; set; }

        [DataMember]
        
        public int purchaser_id { get; set; }

        [DataMember]
        
        public string open_access { get; set; }

        [DataMember]
        
        public string ca_no { get; set; }

        [DataMember]
        
        public string consumer_name { get; set; }

        [DataMember]
        
        public string dt_of_contract { get; set; }

        [DataMember]
        
        public string open_access_type { get; set; }

        [DataMember]
        
        public string type1 { get; set; }

        [DataMember]
        
        public string type_2 { get; set; }

        [DataMember]
        
        public string type_3 { get; set; }

        [DataMember]
        
        public string source { get; set; }

        [DataMember]
        
        public bool unit_wise_schedule { get; set; }

        [DataMember]
        
        public int no_of_units { get; set; }

        [DataMember]
        
        public string delivery_point { get; set; }

        [DataMember]
        
        public string exception_dt_description { get; set; }

        [DataMember]
        
        public decimal min_energy_per { get; set; }

        [DataMember]
        
        public decimal min_capacity_per { get; set; }

        [DataMember]
        
        public decimal availability_per { get; set; }

        [DataMember]
        
        public decimal penalty_rate { get; set; }

        [DataMember]
        
        public decimal ramp_up_rate { get; set; }

        [DataMember]
        
        public decimal ramp_down_rate { get; set; }

        [DataMember]
        
        public bool mod_applicability { get; set; }

        [DataMember]
        
        public bool aux_applicability { get; set; }

        [DataMember]
        
        public bool remc_scheduling { get; set; }

        [DataMember]
        
        public bool peak_offpeak { get; set; }

        [DataMember]
        
        public string user_name { get; set; }

        [DataMember]
        
        public string contract_adjustment { get; set; }

        [DataMember]
        
        public string contract_loi_no { get; set; }

        [DataMember]
        public List<Unit_Details> unit_details { get; set; }
    }

    [DataContract]
    public class Unit_Details
    {
        [DataMember]
        
        public decimal capacity { get; set; }

        [DataMember]
        
        public decimal minimum { get; set; }
    }
}