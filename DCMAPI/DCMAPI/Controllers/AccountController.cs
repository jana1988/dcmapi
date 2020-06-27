using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web.Razor.Generator;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class AccountController : BaseAPIController
    {
        [Authorize]
        [Route("create_user")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> Register(UserInputArgs userModel)
        {
            UserAccountBusinessObject objResult = new UserAccountBusinessObject();
            userModel.created_by = this.GetCurrentUserName();
            return objResult.RegisterNewUser(userModel);
        }

        [Authorize]
        [Route("list_user")]
        [HttpPost]
        public ObjectResultSet<List<UserModel>> GetUser()
        {
            
            UserAccountBusinessObject objResult = new UserAccountBusinessObject();
            return objResult.GetUserDetail();
        }

        [Authorize]
        [Route("get_user")]
        [HttpPost]
        public ObjectResultSet<List<UserModel>> Get_User(UserInputArgs userModel)
        {
            UserAccountBusinessObject objResult = new UserAccountBusinessObject();
            return objResult.GetUserDetailByUserName(userModel);
        }

        [Authorize]
        [Route("edit_user")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> EditUser(UserInputArgs userModel)
        {
            UserAccountBusinessObject objResult = new UserAccountBusinessObject();
            userModel.created_by = this.GetCurrentUserName();
            return objResult.UpdateUser(userModel);
        }

        [Authorize]
        [Route("delete_user")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> DeleteUser(UserInputArgs userModel)
        {
            UserAccountBusinessObject objResult = new UserAccountBusinessObject();
            userModel.created_by = this.GetCurrentUserName();
            return objResult.DeleteUserDetail(userModel);
        }

        [Route("logout")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> Logout()
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            try
            {
                Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer);
                result.success = true;
                result.status = "success";
                result.data = null;
            }
            catch (System.Exception)
            {
                result.success = false;
                result.status = "unsuccess";
                result.data = null;
            }

            return result;
        }
    }
}
