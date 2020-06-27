using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class RoleRightController : BaseAPIController
    {
        [Authorize]
        [Route("create_roleright")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> CreateRoles(UpdateRoleRightsArgs rolerightModel)
        {
            RoleRightBusinessObject objResult = new RoleRightBusinessObject();
            if (rolerightModel == null)
                return new ObjectResultSet<DefaultModel>() { data = null, success = false, status = "Invalid post data." };
            return objResult.CreateRoleRight(rolerightModel);
        }
        
        [Authorize]
        [Route("list_roleright")]
        [HttpPost]
        public ObjectResultSet<List<RoleRightModel>> GetRoleList(RoleRightInputArgs rolerightModel)
        {
            RoleRightBusinessObject objResult = new RoleRightBusinessObject();
            return objResult.GetRoleRightDetail(rolerightModel);
        }

        [Authorize]
        [Route("edit_roleright")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> UpdateRoles(UpdateRoleRightsArgs rolerightModel)
        {
            RoleRightBusinessObject objResult = new RoleRightBusinessObject();
            if (rolerightModel == null)
                return new ObjectResultSet<DefaultModel>() { data = null, success = false, status = "Invalid post data." };
            return objResult.EditRoleRight(rolerightModel);
        }
    }
}
