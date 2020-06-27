using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class FlatRateController : BaseAPIController
    {
        [Authorize]
        [Route("get_flatratelist")]
        [HttpPost]
        public ObjectResultSet<List<FlatRateListModel>> get_flatratelist()
        {
            FlatRateBusinessObject objResult = new FlatRateBusinessObject();
            return objResult.GetFlatRateList();
        }

        [Authorize]
        [Route("create_flatrate")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> create_flatrate(FlatRateArgs model)
        {
            FlatRateBusinessObject objResult = new FlatRateBusinessObject();
            model.updated_By = base.GetCurrentUserName();
            return objResult.CreateFlatRate(model);
        }

        [Authorize]
        [Route("update_flatrate")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> update_flatrate(FlatRateArgs model)
        {
            FlatRateBusinessObject objResult = new FlatRateBusinessObject();
            model.updated_By = base.GetCurrentUserName();
            return objResult.UpdateFlatRate(model);
        }

        [Authorize]
        [Route("get_flatrate")]
        [HttpPost]
        public ObjectResultSet<List<FlateRateModel>> get_flatrate()
        {
            FlatRateBusinessObject objResult = new FlatRateBusinessObject();
            return objResult.GetFlatRateValue();
        }
    }
}