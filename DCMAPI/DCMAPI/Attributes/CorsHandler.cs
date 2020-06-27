using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DCMAPI.Attributes
{
    /// <summary>
    /// Handling pre flight
    /// </summary>
    public class CorsHandler : DelegatingHandler
    {
        private const string HTTPREQUESTMESSAGE = "MS_HttpRequestMessage";
        private const string HTTPCONTEXT = "MS_HttpContext";
        private const string REMOTEENDPOINTMESSAGE = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        /// <summary>
        /// Handling pre flight request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isCorsRequest = true;
            HttpResponseMessage responseMessage = null;
            if (isCorsRequest && request != null)
            {
                bool isPreflightRequest = request.Method == HttpMethod.Options;
                if (isPreflightRequest)
                {
                    responseMessage = await Task.Factory.StartNew<HttpResponseMessage>(() =>
                    {
                        HttpResponseMessage response = new HttpResponseMessage();
                        return response;
                    }, cancellationToken);
                }
                else
                {
                    //if (request.RequestUri.Segments.Where(x => x.ToLower() == "forgotpasswordrequest").FirstOrDefault() == null)
                    //{
                    //    UserInfo identity = ParseAuthorizationHeader(request);
                    //    if (identity == null)
                    //    {
                    //        responseMessage = await Challenge(request, cancellationToken);
                    //    }

                    //    if (!OnAuthorizeRequest(identity, request))
                    //    {
                    //        responseMessage = await Challenge(request, cancellationToken);
                    //    }
                    //}
                    responseMessage = await base.SendAsync(request, cancellationToken);
                }
            }
            else
            {
                //if (request != null)
                //    LogRequest(logInfo, request);
                responseMessage = await base.SendAsync(request, cancellationToken);
            }
            //if (responseMessage != null)
            //    LogResponse(logInfo, responseMessage);
            //if (request != null && responseMessage != null)
            //    LogHelper.Log(logInfo, Enums.Common.LogLevels.Info);
            return responseMessage;
        }


        private async Task<HttpResponseMessage> Challenge(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.Factory.StartNew<HttpResponseMessage>(() =>
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return response;
            }, cancellationToken);
        }
    }
}