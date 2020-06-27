using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class RoleController : BaseAPIController
    {
        [Authorize]
        [Route("create_role")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> CreateRoles(RoleInputArgs roleModel)
        {
            RoleBusinessObject objResult = new RoleBusinessObject();
            roleModel.user_name = this.GetCurrentUserName();
            return objResult.CreateRole(roleModel);
        }
        
        [Authorize]
        [Route("list_role")]
        [HttpPost]
        public ObjectResultSet<List<RoleModel>> GetRoleList(RoleInputArgs roleModel)
        {
            RoleBusinessObject objResult = new RoleBusinessObject();
            roleModel.user_name = this.GetCurrentUserName();
            return objResult.GetRoleDetail(roleModel);
        }

        [Authorize]
        [Route("edit_role")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> UpdateRoles(RoleInputArgs roleModel)
        {
            RoleBusinessObject objResult = new RoleBusinessObject();
            roleModel.user_name = this.GetCurrentUserName();
            return objResult.EditRole(roleModel);
        }

        [Authorize]
        [Route("delete_role")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> DeleteRoles(RoleInputArgs roleModel)
        {
            RoleBusinessObject objResult = new RoleBusinessObject();
            roleModel.user_name = this.GetCurrentUserName();
            return objResult.DeleteRole(roleModel);
        }
    }
}
