using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.Common;
using DCMAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DCMAPI.Controllers
{
    [RoutePrefix("api")]
    public class ContractController : BaseAPIController
    {
        [Authorize]
        [Route("create_stpp")]
        [HttpPost]
        public ObjectResultSet<STPP> create_stpp(ContractSTPPInputArgs stppModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.Create_MasterSTPP(stppModel);
        }
        
        [Authorize]
        [Route("list_stpp")]
        [HttpPost]
        public ObjectResultSet<ContractSTPPModel> list_stpp(ContractSTPPInputArgs stppModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetMasterSTPPDetail(stppModel);
        }

        [Authorize]
        [Route("create_contract_f1")]
        [HttpPost]
        public ObjectResultSet<ContractResponseModel> create_contract_f1(F1ContractCaptureInputArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.ContractCaptureForm1(contractModel);
        }

        [Authorize]
        [Route("get_contract_f1")]
        [HttpPost]
        public ObjectResultSet<F1ContractCaptureModel> get_contract_f1(GetContractDetailArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetContractCaptureForm1(contractModel);
        }

        [Authorize]
        [Route("get_contract_f2")]
        [HttpPost]
        public ObjectResultSet<List<F2ContractCaptureModel>> get_ratehead_f2(GetContractDetailArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetRateHeadForF2(contractModel);
        }

        [Authorize]
        [Route("create_contract_f2")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> create_contract_f1(F2ContractCaptureInputArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.ContractCaptureForm2(contractModel);
        }

        [Authorize]
        [Route("get_contract_f4")]
        [HttpPost]
        public ObjectResultSet<List<F4ContractCaptureModel>> get_ratehead_f4(GetContractDetailArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetRateHeadForF4(contractModel);
        }

        [Authorize]
        [Route("create_contract_f4")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> create_contract_f4(F4ContractCaptureInputArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.ContractCaptureForm4(contractModel);
        }

        [Authorize]
        [Route("create_contract_f3")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> create_contract_f3(F3ContractCaptureArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.ContractCaptureForm3(contractModel);
        }

        [Authorize]
        [Route("get_contract_f3")]
        [HttpPost]
        public ObjectResultSet<List<F3ContractCaptureModel>> get_contract_f3(GetContractDetailArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetContractCaptureForm3(contractModel);
        }

        [Authorize]
        [Route("publish_contract")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> publish_contract(ContractPublishArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            string user = base.GetCurrentLoginId();
            return objResult.ContractCapturePublished(contractModel, user);
        }

        [Authorize]
        [Route("pending_contracts")]
        [HttpPost]
        public ObjectResultSet<List<PendingContractModel>> pending_contracts(PendingContractArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetContractPendingDetail(contractModel);
        }

        [Authorize]
        [Route("list_source")]
        [HttpPost]
        public ObjectResultSet<List<AddendumSourceModel>> list_source()
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.GetContractAddendumSource();
        }

        [Authorize]
        [Route("create_addendum")]
        [HttpPost]
        public ObjectResultSet<DefaultModel> create_addendum(F3ContractCaptureArgs contractModel)
        {
            ContractBusinessObject objResult = new ContractBusinessObject();
            return objResult.CreateAddendum(contractModel);
        }
    }
}
