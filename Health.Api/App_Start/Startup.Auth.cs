using System;
using Health.Data.Auth;
using Health.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Health
{
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new ApplicationOAuthProvider(),
                RefreshTokenProvider = new RefreshTokenProvider()
            };

            app.Use(async (context, next) =>
            {
                var req = context.Request;
                var res = context.Response;
                // for auth2 token requests
                if (req.Path.StartsWithSegments(new PathString("/api/login")))
                {
                    // if there is an origin header
                    var origin = req.Headers.Get("Origin");
                    if (!string.IsNullOrEmpty(origin))
                    {
                        // allow the cross-site request
                        res.Headers.Set("Access-Control-Allow-Origin", origin);
                    }

                    // if this is pre-flight request
                    if (req.Method == "OPTIONS")
                    {
                        // respond immediately with allowed request methods and headers
                        res.StatusCode = 200;
                        res.Headers.AppendCommaSeparatedValues("Access-Control-Allow-Methods", "GET", "POST");
                        res.Headers.AppendCommaSeparatedValues("Access-Control-Allow-Headers", "authorization", "content-type");
                        // no further processing
                        return;
                    }
                }
                // continue executing pipeline
                await next();
            });

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

        }
    }
}
