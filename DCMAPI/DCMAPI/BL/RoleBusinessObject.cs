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
    public class RoleBusinessObject
    {
        public ObjectResultSet<DefaultModel> CreateRole(RoleInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@role_name1", args.role_name),
                                                            new SqlParameter("@user_name1", args.user_name)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_ROLE " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<List<RoleModel>> GetRoleDetail(RoleInputArgs args)
        {
            ObjectResultSet<List<RoleModel>> result = new ObjectResultSet<List<RoleModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@role_id1", args.role_id),
                                                            new SqlParameter("@user_name1", args.user_name),

            };

            SingleResultSet<RoleModel> sglresult = new SingleResultSet<RoleModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_ROLE " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<RoleModel>(strQuery, paramList);
                }
                result.data = sglresult.ResultSet.ToList();
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
        public ObjectResultSet<DefaultModel> EditRole(RoleInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@role_id1", args.role_id),
                                                            new SqlParameter("@user_name1", args.user_name),
                                                            new SqlParameter("@role_name1", args.role_name),
                                                            new SqlParameter("@role_active1", args.role_active)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_ROLE " + MiscUtils.GenerateParameterString(paramList.ToList());
                        ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                        transaction.Commit();
                        result.success = true;
                        result.status = "success";
                    }
                    catch (SqlException ex)
                    {
                        result.success = false;
                        if (ex.Number == 2627)
                            result.status = "Role " + args.role_name + " already exist in systems.";
                        else
                            result.status = ex.Message;
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

        public ObjectResultSet<DefaultModel> DeleteRole(RoleInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Delete),
                                                            new SqlParameter("@role_id1", args.role_id),
                                                            new SqlParameter("@user_name1", args.user_name)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_ROLE " + MiscUtils.GenerateParameterString(paramList.ToList());
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
    }
}