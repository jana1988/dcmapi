using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ExcelDataReader;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class SchedulingController : BaseAPIController
    {
        [Authorize]
        [Route("list_sourceforentitlement")]
        [HttpPost]
        public ObjectResultSet<List<SourceForEntitlement>> list_entitlement()
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetSourcesForEntitlement();
        }

        [Authorize]
        [Route("upload_entitlement")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_entitlement(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    //dtSource.Columns.Add("SOURCE_NAME", typeof(string));
                    dtSource.Columns.Add("UNIT_NAME", typeof(string));
                    dtSource.Columns.Add("UNIT", typeof(decimal));
                    dtSource.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSource.Columns.Add("DATETIME", typeof(string));

                    HttpPostedFile hpf = context.Request.Files[0];

                    string contract_id = Convert.ToString(context.Request.Form["c_id"]);
                    string fDate = Convert.ToString(context.Request.Form["dt"]);
                    if (string.IsNullOrEmpty(contract_id))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select source.", success = false };
                    else if (string.IsNullOrEmpty(fDate))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };
                    int c_id = Convert.ToInt32(contract_id);
                    
                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count < 4)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "from time")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "to time")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][2].ToString().ToLower().Trim() != "time block")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    ObjectResultSet<SchedulingUnitsModel> units = objResult.GetNoOfUnits(c_id);
                    if (units.success)
                    {
                        int excelHeadCount = (dt.Columns.Count - 4);// first 3 From_Time,To_Time, Time_Block and last 1 is Total so, minus 3+1

                        if (!units.data.IsUnitWiseScheduling && excelHeadCount > 1)
                            return new ObjectResultSet<DefaultModel>() { data = null, status = "Entitlement could not be uploaded. More no. of units found. Contract is not unit wise scheduling.", success = false };
                        else if (units.data.IsUnitWiseScheduling && excelHeadCount < units.data.units)
                            return new ObjectResultSet<DefaultModel>() { data = null, status = "Entitlement could not be uploaded. Units mismatched. No of units in contract is: " + units.data.units + "", success = false };
                        else if (units.data.IsUnitWiseScheduling && excelHeadCount > units.data.units)
                        {
                            int colCount = (units.data.units + 3);
                            for (int i = 1; i < dt.Rows.Count; i++)
                            {
                                for (int c = 3; c < colCount; c++)
                                {
                                    DataRow r = dtSource.NewRow();
                                    r["UNIT_NAME"] = dt.Rows[0][c].ToString();
                                    r["TIMEBLOCK"] = Convert.ToInt32(dt.Rows[i][2]);
                                    r["UNIT"] = Convert.ToDecimal(dt.Rows[i][c]);
                                    r["DATETIME"] = fDate;
                                    dtSource.Rows.Add(r);
                                }
                            }

                            result = objResult.UploadEntitlement(dtSource, c_id);
                            if (result.success)
                                result.status = "Entitlement uploaded but units exceeded. No of units in contract is: " + units.data.units + "";
                        }
                        else
                        {
                            for (int i = 1; i < dt.Rows.Count; i++)
                            {
                                for (int c = 3; c < dt.Columns.Count - 1; c++)
                                {
                                    DataRow r = dtSource.NewRow();
                                    r["UNIT_NAME"] = dt.Rows[0][c].ToString();
                                    r["TIMEBLOCK"] = Convert.ToInt32(dt.Rows[i][2]);
                                    r["UNIT"] = Convert.ToDecimal(dt.Rows[i][c]);
                                    r["DATETIME"] = fDate;
                                    dtSource.Rows.Add(r);
                                }
                            }

                            result = objResult.UploadEntitlement(dtSource, c_id);
                        }
                    }
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        [Authorize]
        [Route("upload_system_demand")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_system_demand(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {


                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ID", typeof(int));
                    dtSource.Columns.Add("DT", typeof(string));
                    dtSource.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSource.Columns.Add("DEMAND", typeof(decimal));
                    dtSource.Columns.Add("COC", typeof(decimal));

                    HttpPostedFile hpf = context.Request.Files[0];
                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count != 5)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if(dt.Rows[0][0].ToString().ToLower().Trim() != "id")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "dt")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][2].ToString().ToLower().Trim() != "timeblock")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][3].ToString().ToLower().Trim() != "demand")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][4].ToString().ToLower().Trim() != "coc")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };

                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DataRow r = dtSource.NewRow();
                        r["ID"] = i;
                        r["DT"] = Convert.ToDateTime(dt.Rows[i][1]).ToString("dd/MM/yyyy");
                        r["TIMEBLOCK"] = Convert.ToInt32(dt.Rows[i][2]);
                        r["DEMAND"] = Convert.ToDecimal(dt.Rows[i][3]);
                        r["COC"] = Convert.ToDecimal(dt.Rows[i][4]);
                        dtSource.Rows.Add(r);
                    }
                    SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                    result = objResult.UploadSystenDemand(dtSource);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        [Authorize]
        [Route("upload_iex")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_iex(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ID", typeof(int));
                    dtSource.Columns.Add("DT", typeof(string));
                    dtSource.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSource.Columns.Add("IEX_PUR", typeof(decimal));
                    dtSource.Columns.Add("IEX_SALE", typeof(decimal));

                    HttpPostedFile hpf = context.Request.Files[0];
                    string fDate = Convert.ToString(context.Request.Form["dt"]);
                    if (string.IsNullOrEmpty(fDate))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };

                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count != 3)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "time block")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "iex purchase")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][2].ToString().ToLower().Trim() != "iex sale")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DataRow r = dtSource.NewRow();
                        r["ID"] = i;
                        r["DT"] = fDate;
                        r["TIMEBLOCK"] = Convert.ToInt32(dt.Rows[i][0]);
                        r["IEX_PUR"] = Convert.ToDecimal(dt.Rows[i][1]);
                        r["IEX_SALE"] = Convert.ToDecimal(dt.Rows[i][2]);
                        dtSource.Rows.Add(r);
                    }
                    SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                    result = objResult.UploadIEX(dtSource);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        [Authorize]
        [Route("upload_implementedschedule")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_implementedschedule(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ID", typeof(int));
                    dtSource.Columns.Add("DT", typeof(string));
                    dtSource.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSource.Columns.Add("TOTIME", typeof(string));
                    dtSource.Columns.Add("DEMAND", typeof(decimal));
                    dtSource.Columns.Add("SCHEDULED", typeof(decimal));

                    HttpPostedFile hpf = context.Request.Files[0];
                    string fDate = Convert.ToString(context.Request.Form["dt"]);
                    if (string.IsNullOrEmpty(fDate))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };
                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count != 3)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "time")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "actual demand_g<>t")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][2].ToString().ToLower().Trim() != "total scheduled power")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DataRow r = dtSource.NewRow();
                        r["ID"] = i;
                        r["DT"] = fDate;
                        r["TIMEBLOCK"] = 0;
                        r["TOTIME"] = Convert.ToDateTime(dt.Rows[i][0]).ToString("HH:mm");
                        r["DEMAND"] = Convert.ToDecimal(dt.Rows[i][1]);
                        r["SCHEDULED"] = Convert.ToDecimal(dt.Rows[i][2]);
                        dtSource.Rows.Add(r);
                    }
                    SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                    result = objResult.UploadImplementedSchedule(dtSource);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        [Authorize]
        [Route("confirm_upload_dsm")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> confirm_upload_dsm(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            HttpContext context = HttpContext.Current;

            string fDate = Convert.ToString(context.Request.Form["dt"]);

            if (string.IsNullOrEmpty(fDate))
                return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };

            SchedulingBusinessObject objResult = new SchedulingBusinessObject();

            result = objResult.ConfirmedDSMUpload(fDate);
            return result;
        }

        [Authorize]
        [Route("upload_dsm")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_dsm(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ID", typeof(int));
                    dtSource.Columns.Add("DT", typeof(string));
                    dtSource.Columns.Add("BLOCK", typeof(int));
                    dtSource.Columns.Add("BELOW", typeof(decimal));
                    dtSource.Columns.Add("NOTBELOW", typeof(decimal));
                    dtSource.Columns.Add("W2", typeof(decimal));

                    HttpPostedFile hpf = context.Request.Files[0];
                    string fDate = Convert.ToString(context.Request.Form["dt"]);
                    if (string.IsNullOrEmpty(fDate))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };
                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count < 3)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "below")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "not below")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][2].ToString().ToLower().Trim() != "w2")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DataRow r = dtSource.NewRow();
                        r["ID"] = i;
                        r["DT"] = fDate;
                        r["BLOCK"] = 0;
                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][0])))
                            r["BELOW"] = 0;
                        else
                            r["BELOW"] = Convert.ToDecimal(dt.Rows[i][0]);
                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][1])))
                            r["NOTBELOW"] = 0;
                        else
                            r["NOTBELOW"] = Convert.ToDecimal(dt.Rows[i][1]);
                        r["W2"] = Convert.ToDecimal(dt.Rows[i][2]);
                        dtSource.Rows.Add(r);
                    }
                    SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                    result = objResult.UploadDSM(dtSource);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        [Authorize]
        [Route("upload_remc")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_remc(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    dtSource.Columns.Add("ID", typeof(int));
                    dtSource.Columns.Add("PLANT_NAME", typeof(string));
                    dtSource.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSource.Columns.Add("SCHEDULE_MW", typeof(decimal));

                    DataTable dtSourceTotal = new DataTable();
                    dtSourceTotal.Columns.Add("ID", typeof(int));
                    dtSourceTotal.Columns.Add("DT", typeof(string));
                    dtSourceTotal.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSourceTotal.Columns.Add("SCHEDULE_TOTAL", typeof(decimal));

                    HttpPostedFile hpf = context.Request.Files[0];

                    string source_name = Convert.ToString(context.Request.Form["source_name"]);
                    if (string.IsNullOrEmpty(source_name))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select source name.", success = false };

                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count < 3)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "block no.")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "timestamp")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        for (int c = 2; c < dt.Columns.Count - 1; c++)
                        {
                            DataRow r = dtSource.NewRow();
                            r["ID"] = i;
                            r["PLANT_NAME"] = dt.Rows[0][c].ToString();
                            r["TIMEBLOCK"] = Convert.ToInt32(dt.Rows[i][0]);
                            r["SCHEDULE_MW"] = dt.Rows[i][c].ToString();
                            dtSource.Rows.Add(r);
                        }

                        DataRow rTotal = dtSourceTotal.NewRow();
                        rTotal["ID"] = i;
                        rTotal["TIMEBLOCK"] = Convert.ToInt32(dt.Rows[i][0]);
                        rTotal["DT"] = Convert.ToDateTime(dt.Rows[i][1]).ToString("dd/MM/yyyy");
                        rTotal["SCHEDULE_TOTAL"] = dt.Rows[i][4].ToString();
                        dtSourceTotal.Rows.Add(rTotal);
                    }
                    result = objResult.UploadREMC(source_name, dtSource, dtSourceTotal);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                
            }
            return result;
        }

        [Authorize]
        [Route("list_revision")]
        [HttpPost]
        public ObjectResultSet<List<SchedulingRevision>> list_revision(EntitlementArgs revModel)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetDayAheadRevision(revModel);
        }

        [Authorize]
        [Route("list_generated_scheduling_revision")]
        [HttpPost]
        public ObjectResultSet<List<GenetatedSchedulingRevision>> list_generated_scheduling_revision(EntitlementArgs model)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetGeneratedSchedulingRevision(model);
        }


        [Authorize]
        [Route("get_generated_schedule")]
        [HttpPost]
        public ObjectResultSet<GeneratedScheduleModel> get_generated_schedule(EntitlementArgs model)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetGeneratedSchedule(model);
        }

        [Authorize]
        [Route("final_scheduling_revision")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> final_scheduling_revision(EntitlementArgs model)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.FinalSchedulingRevision(model);
        }


        [Authorize]
        [Route("upload_da_planing")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_da_planing(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtDA = new DataTable();
                    dtDA.Columns.Add("ID", typeof(string));
                    dtDA.Columns.Add("DATETIME", typeof(string));
                    dtDA.Columns.Add("TIMEBLOCK", typeof(int));
                    dtDA.Columns.Add("BASE_CASE_REQUIREMENT", typeof(decimal));
                    dtDA.Columns.Add("ADJUSTMENT", typeof(decimal));
                    dtDA.Columns.Add("REMARK", typeof(string));

                    DataTable dtDAS = new DataTable();
                    dtDAS.Columns.Add("ID", typeof(string));
                    dtDAS.Columns.Add("DATETIME", typeof(string));
                    dtDAS.Columns.Add("TIMEBLOCK", typeof(int));
                    dtDAS.Columns.Add("SOURCE", typeof(string));
                    dtDAS.Columns.Add("UNIT", typeof(decimal));


                    HttpPostedFile hpf = context.Request.Files[0];

                    string fDate = Convert.ToString(context.Request.Form["dt"]);

                    if (string.IsNullOrEmpty(fDate))
                        return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };

                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count < 5)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "fromtime")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "totime")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][2].ToString().ToLower().Trim() != "timeblock")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][3].ToString().ToLower().Trim() != "base case requirement")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][dt.Columns.Count - 2].ToString().ToLower().Trim() != "adjustment")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DataRow dsR = dtDA.NewRow();
                        dsR["ID"] = i;
                        dsR["DATETIME"] = fDate;
                        dsR["TIMEBLOCK"] = dt.Rows[i][2].ToString();
                        if (string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i][3])) || Convert.ToString(dt.Rows[i][dt.Columns.Count - 1]).ToUpper() == "NULL")
                            dsR["BASE_CASE_REQUIREMENT"] = 0;
                        else
                            dsR["BASE_CASE_REQUIREMENT"] = dt.Rows[i][3].ToString();
                        if (string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i][dt.Columns.Count - 2])) || Convert.ToString(dt.Rows[i][dt.Columns.Count - 1]).ToUpper() == "NULL")
                            dsR["ADJUSTMENT"] = 0;
                        else
                            dsR["ADJUSTMENT"] = dt.Rows[i][dt.Columns.Count - 2].ToString();
                        if (string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i][dt.Columns.Count - 1])) || Convert.ToString(dt.Rows[i][dt.Columns.Count - 1]).ToUpper() == "NULL")
                            dsR["REMARK"] = "";
                        else
                            dsR["REMARK"] = dt.Rows[i][dt.Columns.Count - 1].ToString();
                        dtDA.Rows.Add(dsR);

                        for (int c = 4; c < dt.Columns.Count - 2; c++)
                        {
                            DataRow r = dtDAS.NewRow();
                            r["ID"] = i;
                            r["DATETIME"] = fDate;
                            r["TIMEBLOCK"] = dt.Rows[i][2].ToString();
                            r["SOURCE"] = dt.Rows[0][c].ToString();
                            r["UNIT"] = dt.Rows[i][c].ToString();
                            dtDAS.Rows.Add(r);
                        }
                    }
                    result = objResult.UploadDAPlaning(fDate, dtDA, dtDAS);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }


        [Authorize]
        [Route("get_uploaded_revision")]
        [HttpPost]
        public ObjectResultSet<List<SchedulingUploadedModel>> get_uploaded_revision(EntitlementArgs model)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetUploadedRevision(model);
        }

        [Authorize]
        [Route("upload_sldc_schedule")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> upload_sldc_schedule(HttpRequestMessage request)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            HttpContext context = HttpContext.Current;
            try
            {
                SchedulingBusinessObject objResult = new SchedulingBusinessObject();
                if (context.Request.Files.Count > 0)
                {
                    DataTable dtSDLC = new DataTable();
                    dtSDLC.Columns.Add("DT", typeof(string));
                    dtSDLC.Columns.Add("TIMEBLOCK", typeof(int));
                    dtSDLC.Columns.Add("REVISION", typeof(int));
                    dtSDLC.Columns.Add("DEMAND_FORECAST_GT", typeof(decimal));
                    dtSDLC.Columns.Add("TARGET_DRAWAL_SCHEDULE_TD", typeof(decimal));
                    dtSDLC.Columns.Add("TARGET_DRAWAL_SCHEDULE_GT", typeof(decimal));
                    dtSDLC.Columns.Add("DTPS_U1_SCHEDULE", typeof(decimal));
                    dtSDLC.Columns.Add("DTPS_U2_SCHEDULE", typeof(decimal));
                    dtSDLC.Columns.Add("BILATERAL_SOURCE_1", typeof(decimal));
                    dtSDLC.Columns.Add("BILATERAL_SOURCE_2", typeof(decimal));
                    dtSDLC.Columns.Add("CONTRACTED_RE_1", typeof(decimal));
                    dtSDLC.Columns.Add("CONTRACTED_RE_2", typeof(decimal));
                    dtSDLC.Columns.Add("STANDBY", typeof(decimal));
                    dtSDLC.Columns.Add("FIRM_OA", typeof(decimal));
                    dtSDLC.Columns.Add("IEX_PURCHASE", typeof(decimal));
                    dtSDLC.Columns.Add("IEX_SALE", typeof(decimal));
                    dtSDLC.Columns.Add("PX_PURCHASE", typeof(decimal));
                    dtSDLC.Columns.Add("PX_SALE", typeof(decimal));
                    dtSDLC.Columns.Add("RTM_PURCHASE", typeof(decimal));
                    dtSDLC.Columns.Add("RTM_SALE", typeof(decimal));
                    dtSDLC.Columns.Add("OTHER_OA_RE_NON_SOLAR", typeof(decimal));
                    dtSDLC.Columns.Add("OTHER_OA_RE_SOLAR", typeof(decimal));
                    dtSDLC.Columns.Add("REMC_CONTRACTED_WIND", typeof(decimal));
                    dtSDLC.Columns.Add("REMC_CONTRACTED_SOLAR", typeof(decimal));
                    dtSDLC.Columns.Add("REMC_OA_NON_SOLAR", typeof(decimal));
                    dtSDLC.Columns.Add("REMC_OA_SOLAR", typeof(decimal));
                    dtSDLC.Columns.Add("OTHER_1", typeof(decimal));
                    dtSDLC.Columns.Add("OTHER_2", typeof(decimal));
                    dtSDLC.Columns.Add("TOTAL", typeof(decimal));
                    dtSDLC.Columns.Add("SHORTFALL", typeof(decimal));
                    dtSDLC.Columns.Add("SURPLUS", typeof(decimal));



                    HttpPostedFile hpf = context.Request.Files[0];

                    //string fDate = Convert.ToString(context.Request.Form["dt"]);

                    //if (string.IsNullOrEmpty(fDate))
                    //    return new ObjectResultSet<DefaultModel>() { data = null, status = "Please select date.", success = false };

                    Stream stream = hpf.InputStream;

                    IExcelDataReader reader = null;

                    if (hpf.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (hpf.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        result.status = "This file format is not supported";
                        result.success = false;
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var dt = excelRecords.Tables[0];
                    if (dt.Columns.Count < 30)
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][0].ToString().ToLower().Trim() != "delivery date")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][1].ToString().ToLower().Trim() != "time block")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][28].ToString().ToLower().Trim() != "shortfall")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                    if (dt.Rows[0][29].ToString().ToLower().Trim() != "surplus")
                        return new ObjectResultSet<DefaultModel> { data = null, success = false, status = "Invalid template uploaded" };
                  
                    for (int i = 1; i < dt.Rows.Count-1; i++)
                    {
                        DataRow dsR = dtSDLC.NewRow();
                        dsR["DT"] = Convert.ToDateTime(dt.Rows[i][0]).ToString("dd/MM/yyyy").Replace("-","/");
                        dsR["TIMEBLOCK"] = dt.Rows[i][1];
                        dsR["REVISION"] = 0;
                        dsR["DEMAND_FORECAST_GT"] = dt.Rows[i][2];
                        dsR["TARGET_DRAWAL_SCHEDULE_TD"] = dt.Rows[i][3];
                        dsR["TARGET_DRAWAL_SCHEDULE_GT"] = dt.Rows[i][4];
                        dsR["DTPS_U1_SCHEDULE"] = dt.Rows[i][5];
                        dsR["DTPS_U2_SCHEDULE"] = dt.Rows[i][6];
                        dsR["BILATERAL_SOURCE_1"] = dt.Rows[i][7];
                        dsR["BILATERAL_SOURCE_2"] = dt.Rows[i][8];
                        dsR["CONTRACTED_RE_1"] = dt.Rows[i][9];
                        dsR["CONTRACTED_RE_2"] = dt.Rows[i][10];
                        dsR["STANDBY"] = dt.Rows[i][11];
                        dsR["FIRM_OA"] = dt.Rows[i][12];
                        dsR["IEX_PURCHASE"] = dt.Rows[i][13];
                        dsR["IEX_SALE"] = dt.Rows[i][14];
                        dsR["PX_PURCHASE"] = dt.Rows[i][15];
                        dsR["PX_SALE"] = dt.Rows[i][16];
                        dsR["RTM_PURCHASE"] = dt.Rows[i][17];
                        dsR["RTM_SALE"] = dt.Rows[i][18];
                        dsR["OTHER_OA_RE_NON_SOLAR"] = dt.Rows[i][19];
                        dsR["OTHER_OA_RE_SOLAR"] = dt.Rows[i][20];
                        dsR["REMC_CONTRACTED_WIND"] = dt.Rows[i][21];
                        dsR["REMC_CONTRACTED_SOLAR"] = dt.Rows[i][22];
                        dsR["REMC_OA_NON_SOLAR"] = dt.Rows[i][23];
                        dsR["REMC_OA_SOLAR"] = dt.Rows[i][24];
                        dsR["OTHER_1"] = dt.Rows[i][25];
                        dsR["OTHER_2"] = dt.Rows[i][26];
                        dsR["TOTAL"] = dt.Rows[i][27];
                        dsR["SHORTFALL"] = dt.Rows[i][28];
                        dsR["SURPLUS"] = dt.Rows[i][29];
                        dtSDLC.Rows.Add(dsR);
                    }
                    result = objResult.UploadSLDCSchedule(dtSDLC);
                }
                else
                {
                    result.success = false;
                    result.status = "file not found";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
            }
            return result;
        }

        [Authorize]
        [Route("get_sldc_revision")]
        [HttpPost]
        public ObjectResultSet<List<SLDCUploadedModel>> get_sldc_revision(EntitlementArgs model)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetSLDCRevision(model);
        }

        [Authorize]
        [Route("get_sldc_list")]
        [HttpPost]
        public ObjectResultSet<List<SLDCScheduleModel>> get_sldc_list(EntitlementArgs model)
        {
            SchedulingBusinessObject objResult = new SchedulingBusinessObject();
            return objResult.GetSLDCList(model);
        }
    }
}
