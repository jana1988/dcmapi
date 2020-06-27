using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DCMAPI.Common
{
    public class BaseAPIController : ApiController
    {
        private string _token = string.Empty;
        
        protected string GetCurrentUserName()
        {
            return GetValueFromClaimType("UserName");
        }
        protected string GetCurrentLoginId()
        {
            return Convert.ToString(GetValueFromClaimType("LoginId"));
        }
        protected int GetRoleId()
        {
            return Convert.ToInt32(GetValueFromClaimType("RoleId"));
        }
        protected string GetSecurityKey()
        {
            return Convert.ToString(GetValueFromClaimType("SerialNumber"));
        }
        public string PublicIPAddress()
        {
            return System.Configuration.ConfigurationManager.AppSettings["IPAddress"].ToString();
        }
        protected Task<IHttpActionResult> CallWFAsync<T>(Func<T> action)
        {
            IHttpActionResult result = null;
            try
            {
                result = Ok(action.Invoke());
            }
            catch (Exception e1)
            {
                result = InternalServerError(e1);
            }
            return Task.FromResult(result);
        }

        private string GetValueFromClaimType(string claimKey)
        {
            ClaimsIdentity userIdentity = User.Identity as ClaimsIdentity;
            string claimValue = "0";
            IEnumerator<Claim> claimsEnumerator = userIdentity.Claims.GetEnumerator();
            while (claimsEnumerator.MoveNext())
            {
                Claim claim = claimsEnumerator.Current;
                switch (claimKey.ToUpper())
                {
                    case "LOGINID":
                        if (claim.Type == ClaimTypes.NameIdentifier)
                        {
                            claimValue = claim.Value;
                            break;
                        }
                        break;
                    case "USERNAME":
                        if (claim.Type == ClaimTypes.Name)
                        {
                            claimValue = claim.Value;
                            break;
                        }
                        break;
                    case "ROLEID":
                        if (claim.Type == ClaimTypes.Role)
                        {
                            claimValue = claim.Value;
                            break;
                        }
                        break;
                    case "CLIENTID":
                        if (claim.Type == ClaimTypes.Spn)
                        {
                            claimValue = claim.Value;
                            break;
                        }
                        break;
                    case "SERIALNUMBER":
                        if (claim.Type == ClaimTypes.SerialNumber)
                        {
                            claimValue = claim.Value;
                            break;
                        }
                        break;
                }
            }
            return claimValue;
        }

    }
}
