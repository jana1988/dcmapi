using DCMAPI.Arguments;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using DCMAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using static DCMAPI.DBAccess.DCMContext;

namespace DCMAPI.BL
{
    public class AuthBusiness
    {
        public void CreateUser()
        {
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", 1), new SqlParameter("@name1", 1), new SqlParameter("@password1", 1) };
            using (var ctx = new DCMContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        string strQuery = "EXEC sp_createUser " + MiscUtils.GenerateParameterString(paramList.ToList());
                        ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        //public ObjectResultSet<UserModel> GetUserDetails()
        //{
        //    ObjectResultSet<UserModel> result = new ObjectResultSet<UserModel>();
        //    SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", 4) };
        //    SingleResultSet<UserModel> sglresult = new SingleResultSet<UserModel>();

        //    using (var dc = new DataAccess())
        //    {
        //        sglresult = dc.GetSingleResultSet<UserModel>(@"EXEC sp_createUser @mode = @mode1", paramList);
        //    }
        //    result.data = sglresult.ResultSet.ToList().FirstOrDefault();
        //    return result;
        //}
   }
}