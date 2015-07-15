using System;
using System.Threading.Tasks;
using Health.Data.Auth;
using Health.Data.Models;
using Health.Utility;
using Microsoft.Owin.Security.Infrastructure;

namespace Health.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        /// <summary>
        /// This is for users who are already authenticated, persist the refresh token
        /// </summary>
        public string IncomingToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];
            var headerRefreshToken = context.Request.Headers["x-refreshToken"];
            var deviceid = context.Request.Headers["x-deviceId"];

            IncomingToken = headerRefreshToken;

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            using (var repo = new AuthRepository())
            {
                var tokenToLookup = new RefreshToken
                {
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    DeviceId = deviceid,
                };
                var refreshToken = await repo.GenerateRefreshToken(tokenToLookup);
                if (refreshToken == null || string.IsNullOrWhiteSpace(IncomingToken))
                {
                    var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");
                    var refreshTokenId = Guid.NewGuid().ToString("n");

                    var token = new RefreshToken
                    {
                        Id = Helper.GetHash(refreshTokenId),
                        ClientId = clientid,
                        Subject = context.Ticket.Identity.Name,
                        IssuedUtc = DateTime.UtcNow,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime)),
                        DeviceId = deviceid,
                    };

                    context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                    context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                    token.ProtectedTicket = context.SerializeTicket();

                    var result = await repo.AddRefreshToken(token);

                    if (result)
                    {
                        context.SetToken(refreshTokenId);
                    }
                }
                else
                {
                    context.Ticket.Properties.IssuedUtc = refreshToken.IssuedUtc;
                    context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiresUtc;

                    refreshToken.ProtectedTicket = context.SerializeTicket();
                    context.SetToken(IncomingToken);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            IncomingToken = context.Token;
            var hashedTokenId = Helper.GetHash(context.Token);

            using (var repo = new AuthRepository())
            {
                var refreshToken = await repo.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                }
                if (refreshToken != null && string.IsNullOrWhiteSpace(IncomingToken))
                {
                    await repo.RemoveRefreshToken(hashedTokenId);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
        }
    }
}