using DCMAPI.Arguments;
using DCMAPI.Common;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DCMAPI.BL
{
    public class DashboardBusinessObject
    {
     
        public ObjectResultSet<FrequencyModel> GetBlockwiseFrequency()
        {
            FrequencyModel model = new FrequencyModel();
            ObjectResultSet<FrequencyModel> result = new ObjectResultSet<FrequencyModel>();
           
            SingleResultSet<DashboardFrequencyModel> sglresult = new SingleResultSet<DashboardFrequencyModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_BLOCKWISE_FREQUENCY ";
                    sglresult = dc.GetSingleResultSet<DashboardFrequencyModel>(strQuery);
                }
                model.timeblock = sglresult.ResultSet.Select(x => x.TimeBlock).ToArray();
                model.frequency = sglresult.ResultSet.Select(x => x.Frequency).ToArray();
                model.rate = sglresult.ResultSet.Select(x => x.Rate).ToArray();
                result.data = model;
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }
            return result;
        }

        public ObjectResultSet<DeviationModel> GetBlockwiseDeviation()
        {
            DeviationModel model = new DeviationModel();
            ObjectResultSet<DeviationModel> result = new ObjectResultSet<DeviationModel>();

            SingleResultSet<DashboardDeviationModel> sglresult = new SingleResultSet<DashboardDeviationModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_BLOCKWISE_DEVIATION ";
                    sglresult = dc.GetSingleResultSet<DashboardDeviationModel>(strQuery);
                }
                model.timeblock = sglresult.ResultSet.Select(x => x.TimeBlock).ToArray();
                model.deviation = sglresult.ResultSet.Select(x => x.Deviation).ToArray();
                model.rate = sglresult.ResultSet.Select(x => x.Rate).ToArray();
                result.data = model;
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }
            return result;
        }

        public ObjectResultSet<DashboardCurStatModel> GetCurrentBlockStat()
        {
            ObjectResultSet<DashboardCurStatModel> result = new ObjectResultSet<DashboardCurStatModel>();

            SingleResultSet<DashboardCurStatModel> sglresult = new SingleResultSet<DashboardCurStatModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CURRENT_BLOCK_STAT ";
                    sglresult = dc.GetSingleResultSet<DashboardCurStatModel>(strQuery);
                }
                result.data = sglresult.ResultSet.FirstOrDefault();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }
            return result;
        }

        public ObjectResultSet<RTDChart1> GetRTDChart1(DashboarInputArgs args)
        {
            RTDChart1 model = new RTDChart1();
            ObjectResultSet<RTDChart1> result = new ObjectResultSet<RTDChart1>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@DT1", args.date)

            };

            SingleResultSet<RTDChart1Model> sglresult = new SingleResultSet<RTDChart1Model>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_RTD_CHART1 " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<RTDChart1Model>(strQuery, paramList);
                }
                model.Timeblock = sglresult.ResultSet.Select(x => x.Timeblock).ToArray();
                model.Forecast_Demand = sglresult.ResultSet.Select(x => x.Forecast_Demand).ToArray();
                model.Actual_Demand = sglresult.ResultSet.Select(x => x.Actual_Demand).ToArray();
                model.Scheduled_Power = sglresult.ResultSet.Select(x => x.Scheduled_Power).ToArray();
                model.Available_Generation = sglresult.ResultSet.Select(x => x.Available_Generation).ToArray();

                result.data = model;
                result.status = "success";
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        public ObjectResultSet<RTDChart2> GetRTDChart2(DashboarInputArgs args)
        {
            RTDChart2 model = new RTDChart2();
            ObjectResultSet<RTDChart2> result = new ObjectResultSet<RTDChart2>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@DT1", args.date)

            };

            SingleResultSet<RTDChart2Model> sglresult = new SingleResultSet<RTDChart2Model>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_RTD_CHART2 " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<RTDChart2Model>(strQuery, paramList);
                }
                model.BlockNo = sglresult.ResultSet.Select(x => x.BlockNo).ToArray();
                model.Deviation = sglresult.ResultSet.Select(x => x.Deviation).ToArray();
                model.First_Limit = sglresult.ResultSet.Select(x => x.First_Limit).ToArray();
                model.InitiaL_Limit = sglresult.ResultSet.Select(x => x.InitiaL_Limit).ToArray();
                model.Second_Limit = sglresult.ResultSet.Select(x => x.Second_Limit).ToArray();

                result.data = model;
                result.status = "success";
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }
    }
}