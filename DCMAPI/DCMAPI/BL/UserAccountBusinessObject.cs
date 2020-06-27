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
    public class UserAccountBusinessObject
    {
        public ObjectResultSet<DefaultModel> RegisterNewUser(UserInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@created_by1", args.created_by ),
                                                            new SqlParameter("@user_name1", args.user_name),
                                                            new SqlParameter("@role_id1", args.role_id ),
                                                            new SqlParameter("@is_internal1", args.is_internal ),
                                                            new SqlParameter("@password1", MiscUtils.Encrypt(args.password)),
                                                            new SqlParameter("@first_name1", args.first_name ),
                                                            new SqlParameter("@middle_name1", args.middle_name == null? (object)DBNull.Value: args.middle_name),
                                                            new SqlParameter("@last_name1", args.last_name ),
                                                            new SqlParameter("@complete_name1", args.complete_name ),
                                                            new SqlParameter("@job_title1", args.job_title ),
                                                            new SqlParameter("@email1", args.email ),
                                                            new SqlParameter("@mobile1", args.mobile ),
                                                            new SqlParameter("@user_title1", args.user_title)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_USER " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<List<UserModel>> GetUserDetail()
        {
            ObjectResultSet<List<UserModel>> result = new ObjectResultSet<List<UserModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive)
            };

            SingleResultSet<UserModel> sglresult = new SingleResultSet<UserModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_USER " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<UserModel>(strQuery, paramList);
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

        public ObjectResultSet<List<UserModel>> GetUserDetailByUserName(UserInputArgs args)
        {
            ObjectResultSet<List<UserModel>> result = new ObjectResultSet<List<UserModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", 5),
                                                            new SqlParameter("@user_name1", args.user_name),
            };

            SingleResultSet<UserModel> sglresult = new SingleResultSet<UserModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_USER " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<UserModel>(strQuery, paramList);
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

        public ObjectResultSet<DefaultModel> UpdateUser(UserInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Edit),
                                                            new SqlParameter("@created_by1", args.created_by ),
                                                            new SqlParameter("@user_name1", args.user_name),
                                                            new SqlParameter("@role_id1", args.role_id ),
                                                            new SqlParameter("@is_internal1", args.is_internal ),
                                                            new SqlParameter("@password1", MiscUtils.Encrypt(args.password)),
                                                            new SqlParameter("@first_name1", args.first_name ),
                                                            new SqlParameter("@middle_name1", args.middle_name == null? (object)DBNull.Value: args.middle_name),
                                                            new SqlParameter("@last_name1", args.last_name ),
                                                            new SqlParameter("@complete_name1", args.complete_name ),
                                                            new SqlParameter("@job_title1", args.job_title ),
                                                            new SqlParameter("@email1", args.email ),
                                                            new SqlParameter("@mobile1", args.mobile ),
                                                            new SqlParameter("@user_title1", args.user_title)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_USER " + MiscUtils.GenerateParameterString(paramList.ToList());
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

        public ObjectResultSet<DefaultModel> DeleteUserDetail(UserInputArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Delete),
                                                            new SqlParameter("@user_name1", args.user_name)
            };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC USP_USER " + MiscUtils.GenerateParameterString(paramList.ToList());
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