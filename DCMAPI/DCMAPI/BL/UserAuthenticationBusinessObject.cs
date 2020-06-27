using DCMAPI.Arguments;
using DCMAPI.Common;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;
using Microsoft.Owin.Security;

namespace DCMAPI.BL
{
    public class UserAuthenticationBusinessObject
    {
        private LoggedInUserModel autheticatedUser = new LoggedInUserModel();
        public LoggedInUserModel AuthenticateUser(UserLoginInputArgs argsEntity)
        {
            /*For testing only*/
            //AdAuth();
            /*end testing*/

            if (this.IsAuthenticatedUser(argsEntity))
            {
                return autheticatedUser;
            }
            return null;
        }
        private bool IsAuthenticatedUser(UserLoginInputArgs args)
        {
            bool authenticated = false;
            if (args != null)
            {
                List<UserDetails> lstDBList = new List<UserDetails>();
                SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@user_name1", args.UserName),
                                                                new SqlParameter("@password1", MiscUtils.Encrypt(args.UserPassword))
                };
                SingleResultSet<UserDetails> sglresult = new SingleResultSet<UserDetails>();
                try
                {
                    using (var dc = new DCMContext())
                    {
                        sglresult = dc.GetSingleResultSet<UserDetails>(@"EXEC USP_LOGIN_AUTH @user_name = @user_name1, @password = @password1", paramList);
                    }

                    if (sglresult.ResultSet.ToList().Count > 0)
                    {
                        UserDetails model = sglresult.ResultSet.FirstOrDefault();
                        autheticatedUser = new LoggedInUserModel()
                        {
                            user_name = model.user_name,
                            is_internal = model.is_internal,
                            role_id = model.role_id,
                            full_name = model.full_name,
                            first_name = model.first_name,
                            middle_name = model.middle_name,
                            last_name = model.last_name,
                            job_title = model.job_title,
                            email = model.email,
                            mobile = model.mobile,
                            post_code = model.post_code,
                        };
                        authenticated = true;
                    }
                    sglresult = null;
                }
                catch (Exception ex)
                {
                    string mes = ex.Message;
                }
            }
            return authenticated;
        }

    }
}