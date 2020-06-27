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
    public class FlatRateBusinessObject
    {

        public ObjectResultSet<List<FlatRateListModel>> GetFlatRateList()
        {
            ObjectResultSet<List<FlatRateListModel>> result = new ObjectResultSet<List<FlatRateListModel>>();
            SingleResultSet<FlatRateListModel> sglresult = new SingleResultSet<FlatRateListModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_FLAT_RATE_LIST";
                    sglresult = dc.GetSingleResultSet<FlatRateListModel>(strQuery);
                }
                result.data = sglresult.ResultSet.ToList();
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

        public ObjectResultSet<DefaultModel> CreateFlatRate(FlatRateArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@ID1", args.id),
                                                            new SqlParameter("@RATE_ID1", args.rate_id),
                                                            new SqlParameter("@RATE_VALUE1", args.rate_value),
                                                            new SqlParameter("@FROM_DATE1", args.from_date),
                                                            new SqlParameter("@TO_DATE1", args.to_date),
                                                            new SqlParameter("@FROM_TIME1", args.from_time),
                                                            new SqlParameter("@TO_TIME1", args.to_time),
                                                            new SqlParameter("@UPDATED_ON1", args.updated_By),
                                                            new SqlParameter("@REMARKS1", args.remarks),
                                                            new SqlParameter("@CREATED_BY1", args.updated_By),

            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_FLAT_RATE " + MiscUtils.GenerateParameterString(paramList.ToList());
                        var stppId = ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                        transaction.Commit();
                        result.success = true;
                        result.status = "success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.success = false;
                        result.status = ex.Message;
                    }
                }
            }
            return result;
        }

        public ObjectResultSet<DefaultModel> UpdateFlatRate(FlatRateArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@ID1", args.id),
                                                            new SqlParameter("@RATE_ID1", args.rate_id),
                                                            new SqlParameter("@RATE_VALUE1", args.rate_value),
                                                            new SqlParameter("@FROM_DATE1", args.from_date),
                                                            new SqlParameter("@TO_DATE1", args.to_date),
                                                            new SqlParameter("@FROM_TIME1", args.from_time),
                                                            new SqlParameter("@TO_TIME1", args.to_time),
                                                            new SqlParameter("@UPDATED_ON1", args.updated_By),
                                                            new SqlParameter("@REMARKS1", args.remarks)

            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_FLAT_RATE " + MiscUtils.GenerateParameterString(paramList.ToList());
                        var stppId = ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                        transaction.Commit();
                        result.success = true;
                        result.status = "success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.success = false;
                        result.status = ex.Message;
                    }
                }
            }
            return result;
        }

        public ObjectResultSet<List<FlateRateModel>> GetFlatRateValue()
        {
            ObjectResultSet<List<FlateRateModel>> result = new ObjectResultSet<List<FlateRateModel>>();
            SingleResultSet<FlateRateModel> sglresult = new SingleResultSet<FlateRateModel>();
            try
            {

                SqlParameter[] paramList = new SqlParameter[] { 
                    new SqlParameter("@mode1", MiscUtils.Action.Retrive)
                };
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_FLAT_RATE " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<FlateRateModel>(strQuery, paramList);
                }
                result.data = sglresult.ResultSet.ToList();
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