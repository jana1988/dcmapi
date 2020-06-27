using DCMAPI.Arguments;
using DCMAPI.Common;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace DCMAPI.BL
{
    public class SchedulingBusinessObject
    {
        public ObjectResultSet<List<SourceForEntitlement>> GetSourcesForEntitlement()
        {
            ObjectResultSet<List<SourceForEntitlement>> result = new ObjectResultSet<List<SourceForEntitlement>>();
            //SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@MODE1", MiscUtils.Action.Retrive),
            //                                                new SqlParameter("@CONTRACT_ID1", args.contract_id),

            //};

            SingleResultSet<SourceForEntitlement> sglresult = new SingleResultSet<SourceForEntitlement>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CONTRACT_ENTITLEMENT_DDL";
                    sglresult = dc.GetSingleResultSet<SourceForEntitlement>(strQuery);
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

        public ObjectResultSet<SchedulingUnitsModel> GetNoOfUnits(int c_id)
        {
            ObjectResultSet<SchedulingUnitsModel> result = new ObjectResultSet<SchedulingUnitsModel>();

            SingleResultSet<SchedulingUnitsModel> sglresult = new SingleResultSet<SchedulingUnitsModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "SELECT CONTRACT_UNIT_WISE_SCHEDULING, CONTRACT_NO_OF_UNITS FROM CONTRACT_MST_F1  WHERE CONTRACT_ID = " + c_id + "";
                    sglresult = dc.GetSingleResultSet<SchedulingUnitsModel>(strQuery);
                }

                result.data = sglresult.ResultSet.FirstOrDefault();
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

        public ObjectResultSet<DefaultModel> UploadEntitlement(DataTable dtEntitlement, int c_id)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@CONTRACT_ID1", c_id),
                                                            new SqlParameter("@ENTITLEMENT_FILEUPLOAD1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_ENTITLEMENT_FILEUPLOAD",
                                                                Value = dtEntitlement
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_ENTITLEMENT " + MiscUtils.GenerateParameterString(paramList.ToList());
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

       

        public ObjectResultSet<DefaultModel> UploadSystenDemand(DataTable dtEntitlement)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] {
                                                            new SqlParameter("@DEMAND_FILEUPLOAD1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_DEMAND_FILEUPLOAD",
                                                                Value = dtEntitlement
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_SYSTEM_DEMAND " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<DefaultModel> UploadIEX(DataTable dtIEX)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@TT_CONTRACT_IEX1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_IEX",
                                                                Value = dtIEX
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_IEX " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<DefaultModel> UploadImplementedSchedule(DataTable dtImpSchedule)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@CONTRACT_IMPLEMENTED_SCHEDULE1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_IMPLEMENTED_SCHEDULE",
                                                                Value = dtImpSchedule
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_IMPLEMENTED_SCHEDULE " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<DefaultModel> ConfirmedDSMUpload(string date)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            try
            {
                var count = 0;
                using (var dc = new DCMContext())
                {
                    var cnt = dc.Database.SqlQuery<int>("SELECT COUNT(*) CNT FROM CONTRACT_DSM_RATE WHERE [DATETIME] = '" + date + "'");
                    count = cnt.FirstOrDefault();
                }
                if (count > 0)
                {
                    result.status = "File is already uploaded. do you want to upload again?";
                    result.success = true;
                }
                else
                {
                    result.status = "false";
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

        public ObjectResultSet<DefaultModel> UploadDSM(DataTable dtDSM)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@CONTRACT_DSM_RATE1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_DSM_RATE",
                                                                Value = dtDSM
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_DSM_RATE " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<DefaultModel> UploadREMC(string sourceName, DataTable dtREMC, DataTable dtREMCTotal)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@SOURCE_NAME1", sourceName),
                                                            new SqlParameter("@CONTRACT_REMC_PLANT1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_REMC_PLANT",
                                                                Value = dtREMC
                                                            },
                                                            new SqlParameter("@CONTRACT_REMC_TOTAL1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_CONTRACT_REMC_TOTAL",
                                                                Value = dtREMCTotal
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_CONTRACT_REMC_PLANT " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<List<SchedulingRevision>> GetDayAheadRevision(EntitlementArgs args)
        {
            ObjectResultSet<List<SchedulingRevisionModel>> revisionList = GetDayAheadRevisionList(args);
            ObjectResultSet<List<SchedulingRevision>> result = new ObjectResultSet<List<SchedulingRevision>>();
            if (revisionList.success && revisionList.data.Count > 0)
            {
                result.data = revisionList.data.ToList().Select(x => new SchedulingRevision { revision = x.source + " : " + Convert.ToString(x.revision) }).ToList();
                result.status = "success";
                result.success = true;
            }
            else
            {
                result.data = new List<SchedulingRevision>();
                result.status = "Could not get the source & revisions.";
                result.success = false;
            }
            return result;
        }

        public ObjectResultSet<List<SchedulingRevisionModel>> GetDayAheadRevisionList(EntitlementArgs args)
        {
            ObjectResultSet<List<SchedulingRevisionModel>> result = new ObjectResultSet<List<SchedulingRevisionModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@DT1", args.date)

            };

            SingleResultSet<SchedulingRevisionModel> sglresult = new SingleResultSet<SchedulingRevisionModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_DA_AVAILABILITY_REVISION " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<SchedulingRevisionModel>(strQuery, paramList);
                }

                result.data = sglresult.ResultSet.Where(x => x.revision >= 0).ToList();
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

        private ObjectResultSet<DefaultModel> SaveSheduleGeneration(EntitlementArgs args, string ScheduleJson)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            ObjectResultSet<List<SchedulingRevisionModel>> revisionList = GetDayAheadRevisionList(args);
            if (revisionList.success && revisionList.data.Count > 0)
            {
                List<SchedulingRevisionModel> data = revisionList.data;
                string entitlementJson = string.Empty;
                string demandJson = string.Empty;
                string iexJson = string.Empty;
                string remcJson = string.Empty;

                if (data.Where(x => x.r_id == 2).ToList().Count > 0)
                    entitlementJson = Newtonsoft.Json.JsonConvert.SerializeObject(data.Where(x => x.r_id == 2).ToList());
                if (data.Where(x => x.r_id == 1).ToList().Count > 0)
                    demandJson = Newtonsoft.Json.JsonConvert.SerializeObject(data.Where(x => x.r_id == 1).ToList());
                if (data.Where(x => x.r_id == 3).ToList().Count > 0)
                    iexJson = Newtonsoft.Json.JsonConvert.SerializeObject(data.Where(x => x.r_id == 3).ToList());
                if (data.Where(x => x.r_id == 4).ToList().Count > 0)
                    remcJson = Newtonsoft.Json.JsonConvert.SerializeObject(data.Where(x => x.r_id == 4).ToList());

                string date = args.date.Replace("/", "");

                string root = System.Web.HttpContext.Current.Server.MapPath("~/SchedulingJsonData");
                string path = @root + "\\" + date;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, path);
                f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, path);



                string ScheduleJsonPath = "~/SchedulingJsonData/" + date + "/s";
                string entitlementJsonPath = "~/SchedulingJsonData/" + date + "/e";
                string demandJsonPath = "~/SchedulingJsonData/" + date + "/d";
                string iexJsonPath = "~/SchedulingJsonData/" + date + "/i";
                string remcJsonPath = "~/SchedulingJsonData/" + date + "/r";
               
                SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                                new SqlParameter("@DT1", args.date),
                                                                new SqlParameter("@SCHEDULE_JSON1", ScheduleJsonPath),
                                                                new SqlParameter("@ENTITLEMENT_JSON1", entitlementJsonPath),
                                                                new SqlParameter("@DEMAND_JSON1", demandJsonPath),
                                                                new SqlParameter("@IEX_JSON1", iexJsonPath),
                                                                new SqlParameter("@REMC_JSON1", remcJsonPath),
                                                                new SqlParameter("@ISFINAL1", "false"),
                                                                new SqlParameter("@REMARKS1", args.remarks),

                };
                using (var ctx = new DCMContext())
                {
                    using (var transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            ctx.Database.CommandTimeout = 0;
                            string strQuery = "EXEC USP_DAY_AHEAD_SCHEDULE " + MiscUtils.GenerateParameterString(paramList.ToList());
                           
                            int stppId = ctx.Database.SqlQuery<int>(strQuery, paramList).FirstOrDefault();
                            if(stppId == 0)
                            {
                                result.success = false;
                                result.status = "Scheduling generated unsuccessfully for that date. Revision No: " + Convert.ToString(stppId) + "";
                                return result;
                            }
                            // Write that JSON to txt file,  
                            System.IO.File.WriteAllText(path + "/s" + stppId + ".txt", ScheduleJson);
                            System.IO.File.WriteAllText(path + "/e" + stppId + ".txt", entitlementJson);
                            System.IO.File.WriteAllText(path + "/d" + stppId + ".txt", demandJson);
                            System.IO.File.WriteAllText(path + "/i" + stppId + ".txt", iexJson);
                            System.IO.File.WriteAllText(path + "/r" + stppId + ".txt", remcJson);


                            transaction.Commit();
                            result.success = true;
                            result.status = "Scheduling generated successfully. Revision No: " + Convert.ToString(stppId) + "";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            result.success = false;
                            result.status = ex.Message;
                        }
                    }
                }
                
            }
            else
            {
                result.data = new DefaultModel();
                result.status = "Schedule generation could not be saved as source & revisions not found.";
                result.success = false;
            }
            return result;
        }

        public ObjectResultSet<List<GenetatedSchedulingRevision>> GetGeneratedSchedulingRevision(EntitlementArgs args)
        {
            ObjectResultSet<List<GenetatedSchedulingRevision>> result = new ObjectResultSet<List<GenetatedSchedulingRevision>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@DT1", args.date)

            };
            SingleResultSet<GenetatedSchedulingRevision> sglresult = new SingleResultSet<GenetatedSchedulingRevision>();
            try
            {

                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_GENERATED_SCHEDULING_REVISION " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<GenetatedSchedulingRevision>(strQuery, paramList);
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

        public ObjectResultSet<GeneratedScheduleModel> GetGeneratedSchedule(EntitlementArgs args)
        {
            ObjectResultSet<GeneratedScheduleModel> result = new ObjectResultSet<GeneratedScheduleModel>();
            GeneratedScheduleModel resultData = new GeneratedScheduleModel();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@DT1", args.date),
                                                            new SqlParameter("@REVNO1", args.revision),

            };

            SingleResultSet<GeneratedScheduleModel> sglresult = new SingleResultSet<GeneratedScheduleModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_DAY_AHEAD_SCHEDULE " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<GeneratedScheduleModel>(strQuery, paramList);
                }
                if (sglresult.ResultSet.Count > 0)
                {
                    var data = sglresult.ResultSet.FirstOrDefault();
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(data.schedule_output_json + ".txt")))
                        resultData.schedule_output_json = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(data.schedule_output_json + ".txt"));
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(data.entitlement_revision_json + ".txt")))
                        resultData.entitlement_revision_json = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(data.entitlement_revision_json + ".txt"));
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(data.system_demand_revision_json + ".txt")))
                        resultData.system_demand_revision_json = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(data.system_demand_revision_json + ".txt"));
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(data.iex_revision_json + ".txt")))
                        resultData.iex_revision_json = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(data.iex_revision_json + ".txt"));
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(data.remc_revision_json + ".txt")))
                        resultData.remc_revision_json = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(data.remc_revision_json + ".txt"));
                    
                }

                result.data = resultData;
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

        public ObjectResultSet<DefaultModel> FinalSchedulingRevision(EntitlementArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@DT1", args.date),
                                                            new SqlParameter("@REVNO1", args.revision),
                                                           
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_DAY_AHEAD_SCHEDULE " + MiscUtils.GenerateParameterString(paramList.ToList());
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

       

        public ObjectResultSet<DefaultModel> UploadDAPlaning(string date, DataTable dtDAMAdjustment, DataTable dtDAMAdjustmentSource)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@DT1", date),
                                                            new SqlParameter("@DAM_ADJUSTMENT1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_DAM_ADJUSTMENT",
                                                                Value = dtDAMAdjustment
                                                            },
                                                            new SqlParameter("@DAM_ADJUSTMENT_SOURCE1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_DAM_ADJUSTMENT_SOURCE",
                                                                Value = dtDAMAdjustmentSource
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_DAM_UPLOAD " + MiscUtils.GenerateParameterString(paramList.ToList());
                        ctx.Database.ExecuteSqlCommand(strQuery, paramList);
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


        public ObjectResultSet<List<SchedulingUploadedModel>> GetUploadedRevision(EntitlementArgs args)
        {
            ObjectResultSet<List<SchedulingUploadedModel>> result = new ObjectResultSet<List<SchedulingUploadedModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@DT1", args.date),
                                                            new SqlParameter("@TYPE1", args.type),

            };

            SingleResultSet<SchedulingUploadedModel> sglresult = new SingleResultSet<SchedulingUploadedModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_UPLOADED_REVISIONS " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<SchedulingUploadedModel>(strQuery, paramList);
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

        public ObjectResultSet<DefaultModel> UploadSLDCSchedule(DataTable dtSDLC)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@SLDC_SCHEDULE1",  SqlDbType.Structured) {
                                                                TypeName = "dbo.TT_SLDC_SCHEDULE",
                                                                Value = dtSDLC
                                                            }
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_SLDC_SCHEDULE " + MiscUtils.GenerateParameterString(paramList.ToList());
                        ctx.Database.ExecuteSqlCommand(strQuery, paramList);
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

        public ObjectResultSet<List<SLDCUploadedModel>> GetSLDCRevision(EntitlementArgs args)
        {
            ObjectResultSet<List<SLDCUploadedModel>> result = new ObjectResultSet<List<SLDCUploadedModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@DT1", args.date)

            };

            SingleResultSet<SLDCUploadedModel> sglresult = new SingleResultSet<SLDCUploadedModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_SLDC_SCHEDULE_REVISIONS " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<SLDCUploadedModel>(strQuery, paramList);
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

        public ObjectResultSet<List<SLDCScheduleModel>> GetSLDCList(EntitlementArgs args)
        {
            ObjectResultSet<List<SLDCScheduleModel>> result = new ObjectResultSet<List<SLDCScheduleModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                             new SqlParameter("@DT1", args.date),
                                                             new SqlParameter("@REV1", args.revision)
            };

            SingleResultSet<SLDCScheduleModel> sglresult = new SingleResultSet<SLDCScheduleModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_SLDC_SCHEDULE " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<SLDCScheduleModel>(strQuery, paramList);
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