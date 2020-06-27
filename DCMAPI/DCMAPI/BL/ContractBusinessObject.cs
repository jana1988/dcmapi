using DCMAPI.Arguments;
using DCMAPI.Common;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DCMAPI.BL
{
    public class ContractBusinessObject
    {
        public ObjectResultSet<STPP> Create_MasterSTPP(ContractSTPPInputArgs args)
        {
            ObjectResultSet<STPP> result = new ObjectResultSet<STPP>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@type1", args.type),
                                                            new SqlParameter("@title1", args.name)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_STPP " + MiscUtils.GenerateParameterString(paramList.ToList());
                        var stppId = ctx.Database.SqlQuery<int>(strQuery, paramList).FirstOrDefault();
                        transaction.Commit();
                        int id = Convert.ToInt32(stppId);
                        result.data = GetSTPPDetail(id);
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
        private STPP GetSTPPDetail(int stpp_id)
        {
            ObjectResultSet<STPP> result = new ObjectResultSet<STPP>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@stpp_id1", stpp_id)

            };

            SingleResultSet<STPP> sglresult = new SingleResultSet<STPP>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_STPP " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<STPP>(strQuery, paramList);
                }

                result.data = sglresult.ResultSet.ToList().FirstOrDefault();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result.data;
        }
        public ObjectResultSet<ContractSTPPModel> GetMasterSTPPDetail(ContractSTPPInputArgs args)
        {
            ObjectResultSet<ContractSTPPModel> result = new ObjectResultSet<ContractSTPPModel>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive)

            };

            SingleResultSet<ContractSTPPBindModel> sglresult = new SingleResultSet<ContractSTPPBindModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_STPP " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<ContractSTPPBindModel>(strQuery, paramList);
                }
                ContractSTPPModel stpp = new ContractSTPPModel();
                List<STPP> s = sglresult.ResultSet.ToList().Where(y => y.type.Trim().ToLower() == "s").Select(x => new STPP { id = x.id, name = x.name }).ToList();
                stpp.source = new List<STPP>();
                stpp.source = s;

                List<STPP> t = sglresult.ResultSet.ToList().Where(y => y.type.Trim().ToLower() == "t").Select(x => new STPP { id = x.id, name = x.name }).ToList();
                stpp.trade = new List<STPP>();
                stpp.trade = t;

                List<STPP> pur = sglresult.ResultSet.ToList().Where(y => y.type.Trim().ToLower() == "pur").Select(x => new STPP { id = x.id, name = x.name }).ToList();
                stpp.purchase = new List<STPP>();
                stpp.purchase = pur;

                List<STPP> ppa = sglresult.ResultSet.ToList().Where(y => y.type.Trim().ToLower() == "ppa").Select(x => new STPP { id = x.id, name = x.name }).ToList();
                stpp.ppa = new List<STPP>();
                stpp.ppa = ppa;

                result.data = stpp;
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        public ObjectResultSet<ContractResponseModel> ContractCaptureForm1(F1ContractCaptureInputArgs args)
        {
            ObjectResultSet<ContractResponseModel> result = new ObjectResultSet<ContractResponseModel>();
            DataTable dtDetail = new DataTable();
            dtDetail.Columns.Add("capacity", typeof(decimal));
            dtDetail.Columns.Add("minimum", typeof(decimal));

            for (int i = 0; i < args.unit_details.Count(); i++)
            {
                DataRow rw = dtDetail.NewRow();
                rw["capacity"] = args.unit_details[i].capacity;
                rw["minimum"] = args.unit_details[i].minimum;
                dtDetail.Rows.Add(rw);
            }

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_Id),
                                                            new SqlParameter("@CONTRACT_TYPE1", !string.IsNullOrEmpty(args.contract_type) ? args.contract_type : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_PPA_ID1", args.ppa),
                                                            new SqlParameter("@CONTRACT_SOURCE_ID1", args.source_id),
                                                            new SqlParameter("@CONTRACT_TRADER_ID1", args.trader_id),
                                                            new SqlParameter("@CONTRACT_PURCHASER_ID1", args.purchaser_id),
                                                            new SqlParameter("@CONTRACT_CONSUMER_TYPE1", !string.IsNullOrEmpty(args.open_access) ? args.open_access : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_CA_NO1", !string.IsNullOrEmpty(args.ca_no) ? args.ca_no : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_CONSUMER_NAME1", !string.IsNullOrEmpty(args.consumer_name) ? args.consumer_name: (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_DATE_OF_CONTRACT1", !string.IsNullOrEmpty(args.dt_of_contract) ? args.dt_of_contract : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_OPEN_ACCESS_TYPE1", !string.IsNullOrEmpty(args.open_access_type) ? args.open_access_type : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_TYPE11", !string.IsNullOrEmpty(args.type1) ? args.type1 : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_TYPE21", !string.IsNullOrEmpty(args.type_2) ? args.type_2 : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_TYPE31", !string.IsNullOrEmpty(args.type_3) ? args.type_3 : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_SOURCE1",!string.IsNullOrEmpty(args.source) ? args.source : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_UNIT_WISE_SCHEDULING1", args.unit_wise_schedule),
                                                            new SqlParameter("@CONTRACT_NO_OF_UNITS1", args.no_of_units),
                                                            new SqlParameter("@CONTRACT_DELIVERY_POINT1", !string.IsNullOrEmpty(args.delivery_point) ? args.delivery_point : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_EXCEPTION1", !string.IsNullOrEmpty(args.exception_dt_description) ? args.exception_dt_description : (object)DBNull.Value),
                                                            new SqlParameter("@CONTRACT_MIN_ENERGY_OFFTAKE1", args.min_energy_per),
                                                            new SqlParameter("@CONTRACT_MIN_CAPACITY_OFFTAKE1", args.min_capacity_per),
                                                            new SqlParameter("@CONTRACT_AVAILABILITY_FOR_FC1", args.availability_per),
                                                            new SqlParameter("@CONTRACT_PENALTY_RATE1", args.penalty_rate),
                                                            new SqlParameter("@CONTRACT_RAMP_UP_RATE1", args.ramp_up_rate),
                                                            new SqlParameter("@CONTRACT_RAMP_DOWN_RATE1", args.ramp_down_rate),
                                                            new SqlParameter("@CONTRACT_MOD_APPLICABILITY1", args.mod_applicability),
                                                            new SqlParameter("@CONTRACT_AUX_APPLICABILITY1", args.aux_applicability),
                                                            new SqlParameter("@CONTRACT_REMC_SCHEDULING_ENABLED1", args.remc_scheduling),
                                                            new SqlParameter("@CONTRACT_PEAK_OFFPEAK1", args.peak_offpeak),
                                                            new SqlParameter("@CONTRACT_ADJUSTMENT1", args.contract_adjustment),
                                                            new SqlParameter("@CREATED_BY1", args.user_name),
                                                            new SqlParameter("@CONTRACT_LOI_NO1", args.contract_loi_no),
                                                            new SqlParameter("@UNIT_DETAILS1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_UNIT_DETAILS",
                                                                Value = dtDetail
                                                            }

            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_MST_F1 " + MiscUtils.GenerateParameterString(paramList.ToList());
                        var c_Id = ctx.Database.SqlQuery<int>(strQuery, paramList).FirstOrDefault();
                        transaction.Commit();
                        int id = Convert.ToInt32(c_Id);
                        result.data = new ContractResponseModel { contract_id = c_Id };
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
        public ObjectResultSet<F1ContractCaptureModel> GetContractCaptureForm1(GetContractDetailArgs args)
        {
            ObjectResultSet<F1ContractCaptureModel> result = new ObjectResultSet<F1ContractCaptureModel>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@MODE1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),

            };

            SingleResultSet<F1ContractCaptureModel> sglresult = new SingleResultSet<F1ContractCaptureModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_MST_F1 " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<F1ContractCaptureModel>(strQuery, paramList);
                }
                if (sglresult.ResultSet.Count > 0)
                {
                    result.data = sglresult.ResultSet.FirstOrDefault();
                    var unitDetail = GetUnitDetail(args.contract_id);
                    if (unitDetail.Count > 0)
                        result.data.unit_details = unitDetail;
                    result.status = "success";
                    result.success = true;
                }
                else {
                    result.status = "no record fouund.";
                    result.success = false;
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }
        private List<UnitDetailsModel> GetUnitDetail(int contract_id)
        {
            List<UnitDetailsModel> result = new List<UnitDetailsModel>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@CONTRACT_ID1", contract_id)

            };

            SingleResultSet<UnitDetailsModel> sglresult = new SingleResultSet<UnitDetailsModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_UNIT_DTL " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<UnitDetailsModel>(strQuery, paramList);
                }

                result = sglresult.ResultSet.ToList();
            }
            catch (Exception)
            {
                result = new List<UnitDetailsModel>();
            }
            return result;
        }


        public ObjectResultSet<List<F2ContractCaptureModel>> GetRateHeadForF2(GetContractDetailArgs args)
        {
            ObjectResultSet<List<F2ContractCaptureModel>> result = new ObjectResultSet<List<F2ContractCaptureModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@MODE1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@FORM1", 2),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),

            };

            SingleResultSet<F2ContractCaptureModel> sglresult = new SingleResultSet<F2ContractCaptureModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_RATE_F2_F4 " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<F2ContractCaptureModel>(strQuery, paramList);
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
        public ObjectResultSet<DefaultModel> ContractCaptureForm2(F2ContractCaptureInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            DataTable dtApplicable = new DataTable();
            dtApplicable.Columns.Add("ID", typeof(int));
            dtApplicable.Columns.Add("APPLICABLE", typeof(bool));

            for (int i = 0; i < args.f2detail.Count(); i++)
            {
                DataRow rw = dtApplicable.NewRow();
                rw["ID"] = args.f2detail[i].id;
                rw["APPLICABLE"] = args.f2detail[i].applicable;
                dtApplicable.Rows.Add(rw);
            }
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@FORM1", 2),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),
                                                            new SqlParameter("@USER1", args.user_name),
                                                            new SqlParameter("@CONTRACT_CAPTURE_FORM21",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_CAPTURE_FORM2",
                                                                Value = dtApplicable
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_RATE_F2_F4 " + MiscUtils.GenerateParameterString(paramList.ToList());
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


        public ObjectResultSet<List<F4ContractCaptureModel>> GetRateHeadForF4(GetContractDetailArgs args)
        {
            ObjectResultSet<List<F4ContractCaptureModel>> result = new ObjectResultSet<List<F4ContractCaptureModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@MODE1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@FORM1", 4),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),

            };

            SingleResultSet<F4ContractCaptureModel> sglresult = new SingleResultSet<F4ContractCaptureModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_RATE_F2_F4 " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<F4ContractCaptureModel>(strQuery, paramList);
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
        public ObjectResultSet<DefaultModel> ContractCaptureForm4(F4ContractCaptureInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            DataTable dtF4Detail = new DataTable();
            dtF4Detail.Columns.Add("ID", typeof(int));
            dtF4Detail.Columns.Add("RATE_VALUE", typeof(decimal));
            dtF4Detail.Columns.Add("FROM_DATE", typeof(string));
            dtF4Detail.Columns.Add("TO_DATE", typeof(string));
            dtF4Detail.Columns.Add("FROM_TIME", typeof(string));
            dtF4Detail.Columns.Add("TO_TIME", typeof(string));
            dtF4Detail.Columns.Add("UPDATED_ON", typeof(string));
            dtF4Detail.Columns.Add("REMARKS", typeof(string));
            dtF4Detail.Columns.Add("EDITED_ON", typeof(string));
            dtF4Detail.Columns.Add("EDITED_BY", typeof(string));

            for (int i = 0; i < args.f4detail.Count(); i++)
            {
                DataRow rw = dtF4Detail.NewRow();
                rw["ID"] = args.f4detail[i].id;
                rw["RATE_VALUE"] = args.f4detail[i].rate_value;
                rw["FROM_DATE"] = args.f4detail[i].from_date;
                rw["TO_DATE"] = args.f4detail[i].to_date;
                rw["FROM_TIME"] = args.f4detail[i].from_time;
                rw["TO_TIME"] = args.f4detail[i].to_time;
                rw["UPDATED_ON"] = args.f4detail[i].updated_on;
                rw["REMARKS"] = args.f4detail[i].remarks;
                rw["EDITED_ON"] = "";
                rw["EDITED_BY"] = args.user_name;
                dtF4Detail.Rows.Add(rw);
            }
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@FORM1", 4),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),
                                                            new SqlParameter("@USER1", args.user_name),
                                                            new SqlParameter("@CONTRACT_CAPTURE_FORM41",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_CAPTURE_FORM4",
                                                                Value = dtF4Detail
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_RATE_F2_F4 " + MiscUtils.GenerateParameterString(paramList.ToList());
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


        public ObjectResultSet<DefaultModel> ContractCaptureForm3(F3ContractCaptureArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            DataTable dtF3Detail = new DataTable();
            dtF3Detail.Columns.Add("ADDENDUM_ID", typeof(int));
            dtF3Detail.Columns.Add("FROM_DATE", typeof(string));
            dtF3Detail.Columns.Add("TO_DATE", typeof(string));
            dtF3Detail.Columns.Add("FROM_TIME", typeof(string));
            dtF3Detail.Columns.Add("TO_TIME", typeof(string));
            dtF3Detail.Columns.Add("CONTRACTED_CAPACITY", typeof(decimal));
            dtF3Detail.Columns.Add("OA_CAPACITY", typeof(decimal));
            dtF3Detail.Columns.Add("CONTRACTED_RATE", typeof(string));
            dtF3Detail.Columns.Add("DISCOUNT", typeof(decimal));

            for (int i = 0; i < args.f3detail.Count(); i++)
            {
                DataRow rw = dtF3Detail.NewRow();
                rw["ADDENDUM_ID"] = args.f3detail[i].addendum_id;
                rw["FROM_DATE"] = args.f3detail[i].from_date;
                rw["TO_DATE"] = args.f3detail[i].to_date;
                rw["FROM_TIME"] = args.f3detail[i].from_time;
                rw["TO_TIME"] = args.f3detail[i].to_time;
                rw["CONTRACTED_CAPACITY"] = args.f3detail[i].contracted_capacity;
                rw["OA_CAPACITY"] = args.f3detail[i].oa_capacity;
                rw["CONTRACTED_RATE"] = args.f3detail[i].contracted_rate;
                rw["DISCOUNT"] = args.f3detail[i].discount;
                dtF3Detail.Rows.Add(rw);
            }
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),
                                                            new SqlParameter("@CREATED_BY1", args.user_name),
                                                            new SqlParameter("@CONTRACT_CAPTURE_FORM31",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_CAPTURE_FORM3",
                                                                Value = dtF3Detail
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_ADDENDUM_F3 " + MiscUtils.GenerateParameterString(paramList.ToList());
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
        public ObjectResultSet<List<F3ContractCaptureModel>> GetContractCaptureForm3(GetContractDetailArgs args)
        {
            ObjectResultSet<List<F3ContractCaptureModel>> result = new ObjectResultSet<List<F3ContractCaptureModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@MODE1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),

            };

            SingleResultSet<F3ContractCaptureModel> sglresult = new SingleResultSet<F3ContractCaptureModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_ADDENDUM_F3 " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<F3ContractCaptureModel>(strQuery, paramList);
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

        public ObjectResultSet<DefaultModel> ContractCapturePublished(ContractPublishArgs args, string user)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),
                                                            new SqlParameter("@CONTRACT_PUBLISHED1", args.publish),
                                                            new SqlParameter("@CREATED_BY1", user)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_MST_F1 " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<List<PendingContractModel>> GetContractPendingDetail(PendingContractArgs args)
        {
            ObjectResultSet<List<PendingContractModel>> result = new ObjectResultSet<List<PendingContractModel>>();
            SingleResultSet<PendingContractModel> sglresult = new SingleResultSet<PendingContractModel>();
            try
            {
                SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@iscomplete1", args.iscomplete) };

                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_PENDING @iscomplete = @iscomplete1";
                    sglresult = dc.GetSingleResultSet<PendingContractModel>(strQuery, paramList);
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

        public ObjectResultSet<List<AddendumSourceModel>> GetContractAddendumSource()
        {
            ObjectResultSet<List<AddendumSourceModel>> result = new ObjectResultSet<List<AddendumSourceModel>>();
            SingleResultSet<AddendumSourceModel> sglresult = new SingleResultSet<AddendumSourceModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_ADDENDUM_SOURCE";
                    sglresult = dc.GetSingleResultSet<AddendumSourceModel>(strQuery);
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

        public ObjectResultSet<DefaultModel> CreateAddendum(F3ContractCaptureArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            DataTable dtF3Detail = new DataTable();
            dtF3Detail.Columns.Add("ADDENDUM_ID", typeof(int));
            dtF3Detail.Columns.Add("FROM_DATE", typeof(string));
            dtF3Detail.Columns.Add("TO_DATE", typeof(string));
            dtF3Detail.Columns.Add("FROM_TIME", typeof(string));
            dtF3Detail.Columns.Add("TO_TIME", typeof(string));
            dtF3Detail.Columns.Add("CONTRACTED_CAPACITY", typeof(decimal));
            dtF3Detail.Columns.Add("OA_CAPACITY", typeof(decimal));
            dtF3Detail.Columns.Add("CONTRACTED_RATE", typeof(string));
            dtF3Detail.Columns.Add("DISCOUNT", typeof(decimal));

            for (int i = 0; i < args.f3detail.Count(); i++)
            {
                DataRow rw = dtF3Detail.NewRow();
                rw["ADDENDUM_ID"] = args.f3detail[i].addendum_id;
                rw["FROM_DATE"] = args.f3detail[i].from_date;
                rw["TO_DATE"] = args.f3detail[i].to_date;
                rw["FROM_TIME"] = args.f3detail[i].from_time;
                rw["TO_TIME"] = args.f3detail[i].to_time;
                rw["CONTRACTED_CAPACITY"] = args.f3detail[i].contracted_capacity;
                rw["OA_CAPACITY"] = args.f3detail[i].oa_capacity;
                rw["CONTRACTED_RATE"] = args.f3detail[i].contracted_rate;
                rw["DISCOUNT"] = args.f3detail[i].discount;
                dtF3Detail.Rows.Add(rw);
            }
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@CONTRACT_ID1", args.contract_id),
                                                            new SqlParameter("@CREATED_BY1", args.user_name),
                                                            new SqlParameter("@CONTRACT_CAPTURE_FORM31",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_CAPTURE_FORM3",
                                                                Value = dtF3Detail
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_ADDENDUM_F3 " + MiscUtils.GenerateParameterString(paramList.ToList());
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
    }
}