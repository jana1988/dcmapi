using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class ScreenCheckController : BaseAPIController
    {
        
        [Authorize]
        [Route("check_screen_access")]
        [HttpPost]
        public ObjectResultSet<List<ScreenCheckModel>> GetRoleList(ScreenCheckInputArgs screenModel)
        {
            ScreenCheckBusinessObject objResult = new ScreenCheckBusinessObject();
            return objResult.CheckScreenDetail(screenModel);
        }

        [Authorize]
        [Route("get_screen")]
        [HttpPost]
        public ObjectResultSet<List<ScreenModel>> GetScreenList(ScreenCheckInputArgs screenModel)
        {
            ScreenCheckBusinessObject objResult = new ScreenCheckBusinessObject();
            return objResult.GetScreenDetail(screenModel);
        }

        [Authorize]
        [Route("get_menu")]
        [HttpPost]
        public ObjectResultSet<List<MenuModel>> GetScreenList(RoleRightInputArgs roleRightseModel)
        {
            ScreenCheckBusinessObject objResult = new ScreenCheckBusinessObject();
            return objResult.GetMenuDetail(roleRightseModel);
        }

    }
}
