using DCMAPI.Arguments;
using DCMAPI.BL;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(DCMAPI.Startup))]

namespace DCMAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //HttpConfiguration config = new HttpConfiguration();
            //OAuthBearerAuthenticationOptions oauthOptions = new OAuthBearerAuthenticationOptions();
            //app.UseOAuthBearerAuthentication(oauthOptions);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);
            WebApiConfig.Register(new HttpConfiguration());

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<AuthContext>(() => new AuthContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            private LoggedInUserModel userInfo = null;
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                UserAuthenticationBusinessObject userObject = new UserAuthenticationBusinessObject();
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                UserLoginInputArgs userArg = new UserLoginInputArgs() { UserName = context.UserName, UserPassword = context.Password, IsToken = true };
                userInfo = null;
                userInfo = userObject.AuthenticateUser(userArg);
                if (userInfo == null)
                {
                    context.SetError("access_denied", "You are not authorized to access. Please contact System Administrator for getting access.");
                    return;
                }
                else
                {
                    try
                    {
                        IdentityUser user = new IdentityUser { UserName = userInfo.user_name, Email = userInfo.email, PhoneNumber = userInfo.mobile };
                        ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        IDictionary<string, string> CustomProperties = new Dictionary<string, string>
                        {
                            //{ "user_name", userInfo.user_name },
                            { "is_internal", Convert.ToString(userInfo.is_internal) },
                            { "full_name", userInfo.full_name },
                            { "first_name", userInfo.first_name },
                            { "middle_name", userInfo.middle_name },
                            { "last_name", userInfo.last_name },
                            { "job_title", userInfo.job_title },
                            //{ "role_id", Convert.ToString(userInfo.role_id) },
                            //{ "email", userInfo.email },
                            { "mobile", Convert.ToString(userInfo.mobile) },
                            { "post_code", userInfo.post_code }
                        };
                        AuthenticationProperties properties = CreateProperties(CustomProperties);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.user_name));
                        identity.AddClaim(new Claim(ClaimTypes.Role, Convert.ToString(userInfo.role_id)));
                        identity.AddClaim(new Claim(ClaimTypes.SerialNumber, userInfo.email));

                        var ticket = new AuthenticationTicket(identity, properties);
                        context.Validated(ticket);
                        await Task.FromResult(0);
                    }
                    catch (Exception ex)
                    {
                        context.SetError("access_denied", ex.Message);
                        return;
                    }
                }
                //else
                //{
                //    context.SetError("invalid_grant", "Invalid UserId or password'");
                //    context.Rejected();
                //}
            }
        }

        private static UserManager<IdentityUser> CreateManager(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<IdentityUser>(context.Get<AuthContext>());
            var owinManager = new UserManager<IdentityUser>(userStore);
            return owinManager;
        }
        public static AuthenticationProperties CreateProperties(IDictionary<string, string> customProperties)
        {
            return new AuthenticationProperties(customProperties);
        }
    }
}
