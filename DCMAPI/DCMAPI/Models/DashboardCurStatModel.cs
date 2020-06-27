using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DCMAPI.Models
{
    [DataContract]
    public class DashboardCurStatModel
    {
        [DataMember(Order = 1), Column("TimeBlock")]
        public string TimeBlock { get; set; }

        [DataMember(Order = 2), Column("TimeBlockNo")]
        public string TimeBlockNo { get; set; }

        [DataMember(Order = 3), Column("CurrentTime")]
        public string CurrentTime { get; set; }

        [DataMember(Order = 4), Column("ElapsedTime")]
        public string ElapsedTime { get; set; }

        [DataMember(Order = 5), Column("RemainingTime")]
        public string RemainingTime { get; set; }

        [DataMember(Order = 6), Column("ScheduledDemand")]
        public string ScheduledDemand { get; set; }

        [DataMember(Order = 7), Column("ScheduledPower")]
        public string ScheduledPower { get; set; }

        [DataMember(Order = 8), Column("ActualDemandAvg")]
        public string ActualDemandAvg { get; set; }

        [DataMember(Order = 9), Column("ActualDemandInst")]
        public string ActualDemandInst { get; set; }

        [DataMember(Order = 10), Column("DeviationAvg")]
        public string DeviationAvg { get; set; }

        [DataMember(Order = 11), Column("DeviationInst")]
        public string DeviationInst { get; set; }

        [DataMember(Order = 12), Column("FrequencyAvg")]
        public string FrequencyAvg { get; set; }

        [DataMember(Order = 13), Column("FrequencyInst")]
        public string FrequencyInst { get; set; }

        [DataMember(Order = 14), Column("DSMRateAvg")]
        public string DSMRateAvg { get; set; }

        [DataMember(Order = 15), Column("DSMRateInst")]
        public string DSMRateInst { get; set; }

        [DataMember(Order = 16), Column("DeviationchargesAvg")]
        public string DeviationchargesAvg { get; set; }

        [DataMember(Order = 17), Column("DeviationchargesInst")]
        public string DeviationchargesInst { get; set; }

        [DataMember(Order = 18), Column("AddlDeviationChargesAvg")]
        public string AddlDeviationChargesAvg { get; set; }

        [DataMember(Order = 19), Column("AddlDeviationChargesInst")]
        public string AddlDeviationChargesInst { get; set; }

        [DataMember(Order = 20), Column("CapUI")]
        public string CapUI { get; set; }

        [DataMember(Order = 21), Column("SameSignDevCount")]
        public string SameSignDevCount { get; set; }

        [DataMember(Order = 22), Column("SignViolationChargesLikely")]
        public string SignViolationChargesLikely { get; set; }

        [DataMember(Order = 23), Column("TotalChargesAvg")]
        public string TotalChargesAvg { get; set; }

        [DataMember(Order = 24), Column("TotalChargesInst")]
        public string TotalChargesInst { get; set; }

        [DataMember(Order = 25), Column("AskingRate")]
        public string AskingRate { get; set; }


        [DataMember(Order = 26), Column("Parameter1")]
        public string Parameter1 { get; set; }

        [DataMember(Order = 27), Column("Violation1")]
        public string Violation1 { get; set; }

        [DataMember(Order = 28), Column("Message1")]
        public string Message1 { get; set; }

        [DataMember(Order = 29), Column("Parameter2")]
        public string Parameter2 { get; set; }

        [DataMember(Order = 30), Column("Violation2")]
        public string Violation2 { get; set; }

        [DataMember(Order = 31), Column("Message2")]
        public string Message2 { get; set; }

        [DataMember(Order = 32), Column("Parameter3")]
        public string Parameter3 { get; set; }

        [DataMember(Order = 33), Column("Violation3")]
        public string Violation3 { get; set; }

        [DataMember(Order = 34), Column("Message3")]
        public string Message3 { get; set; }

        [DataMember(Order = 35), Column("Condition")]
        public string Condition { get; set; }

        [DataMember(Order = 36), Column("MaharashtraUDOD")]
        public string MaharashtraUDOD { get; set; }

        [DataMember(Order = 37), Column("ODCountAbove1stLimit")]
        public string ODCountAbove1stLimit { get; set; }

        [DataMember(Order = 38), Column("MarginalRateofPower")]
        public string MarginalRateofPower { get; set; }

        [DataMember(Order = 39), Column("AverageRateofPowerExclFC")]
        public string AverageRateofPowerExclFC { get; set; }

        [DataMember(Order = 40), Column("AverageRateofPowerInclFC")]
        public string AverageRateofPowerInclFC { get; set; }

        [DataMember(Order = 41), Column("IEXRate")]
        public string IEXRate { get; set; }

        [DataMember(Order = 42), Column("PXRate")]
        public string PXRate { get; set; }

        [DataMember(Order = 43), Column("RealTimeExchangeRate")]
        public string RealTimeExchangeRate { get; set; }
    }


}