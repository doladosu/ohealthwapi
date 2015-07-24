using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Health.Setup
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageHandler : DelegatingHandler
    {
        const string Origin = "Origin";
        const string RequestMethod = "Access-Control-Request-Method";
        const string RequestHeaders = "Access-Control-Request-Headers";
        const string AllowOrigin = "Access-Control-Allow-Origin";
        const string AllowMethods = "Access-Control-Allow-Methods";
        const string AllowHeaders = "Access-Control-Allow-Headers";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var isOptionsRequest = request.Headers.Contains("Origin") && (request.Method == HttpMethod.Options);

            if (isOptionsRequest)
            {
                return await Task.Factory.StartNew(() =>
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Headers.Add(AllowOrigin, request.Headers.GetValues(Origin).First());

                    var accessControlRequestMethod = request.Headers.GetValues(RequestMethod).FirstOrDefault();
                    if (accessControlRequestMethod != null)
                    {
                        response.Headers.Add(AllowMethods, RequestMethod);
                    }

                    var requestedHeaders = string.Join(", ", request.Headers.GetValues(RequestHeaders));
                    if (!string.IsNullOrEmpty(requestedHeaders))
                    {
                        response.Headers.Add(AllowHeaders, requestedHeaders);
                    }

                    return response;
                }, cancellationToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}